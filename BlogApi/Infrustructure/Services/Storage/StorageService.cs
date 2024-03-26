using Blog.Application.Abstractions.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrustructure.Services.Storage
{
    public class StorageService : IStorageService
    {
        readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public string storageName { get => _storage.GetType().Name; }

        public async Task deleteAsync(string path)
        {
            await _storage.deleteAsync(path);
        }

        public DirectoryInfo dir(string path)
        {
            return _storage.dir(path);
        }

        public List<string> getFiles(string pathOrContainer)
        {
            return _storage.getFiles(pathOrContainer);
        }

        public bool hasFile(string pathOrContainer, string fileName)
        {
            return _storage.hasFile(pathOrContainer, fileName);
        }

        public async Task<(string fileName, string path)> uploadAsync(string pathOrContainer, IFormFile file)
        {
            return await _storage.uploadAsync(pathOrContainer, file);
        }
    }
}
