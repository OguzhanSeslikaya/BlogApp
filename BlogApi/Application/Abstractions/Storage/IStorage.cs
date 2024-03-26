using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Abstractions.Storage
{
    public interface IStorage
    {
        Task<(string fileName, string path)> uploadAsync(string pathOrContainer, IFormFile file);
        Task deleteAsync(string path);
        List<string> getFiles(string pathOrContainer);
        bool hasFile(string pathOrContainer, string fileName);
        DirectoryInfo dir(string path);
    }
}
