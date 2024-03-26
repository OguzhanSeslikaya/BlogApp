using Blog.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrustructure.Services.Storage.Local
{
    public class LocalStorage : ILocalStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task deleteAsync(string path)
        {
            File.Delete(path);
        }

        public List<string> getFiles(string pathOrContainer)
        {
            DirectoryInfo dir = new DirectoryInfo(pathOrContainer);
            return dir.GetFiles().Select(f => f.Name).ToList();
        }

        public bool hasFile(string pathOrContainer, string fileName)
        {
            return File.Exists($"{pathOrContainer}\\{fileName}");
        }

        public async Task<(string fileName, string path)> uploadAsync(string pathOrContainer, IFormFile file)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, pathOrContainer);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            string newName = fileRenameAsync(uploadPath, file.FileName);
            bool kontrol = await copyFileAsync(Path.Combine(uploadPath, newName), file);
            if (kontrol)
            {
                return (newName, $"{uploadPath}\\{newName}");
            }
            
            return ("0", "0");
        }

        private string fileRenameAsync(string path, string fileName)
        {

            string extension = Path.GetExtension(fileName);
            string oldName = Path.GetFileNameWithoutExtension(fileName);
            int ek = 0;
            string newName;
            while (true)
            {
                ek++;
                newName = $"{oldName}-{ek}{extension}";
                if (!File.Exists($"{path}\\{newName}"))
                {
                    return newName;
                }
            }
        }

        private async Task<bool> copyFileAsync(string path, IFormFile file)
        {
            try
            {
                using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DirectoryInfo dir(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            return dir;
        }
    }
}
