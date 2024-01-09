using ITSTEPRabbitMQ.Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ITSTEPRabbitMQ.Storage.Clients
{
    public class LocalStorageClient : IStorageClient
    {
        private readonly string _baseDirectory;

        public LocalStorageClient(string baseDirectory)
        {
            _baseDirectory = baseDirectory;
        }

        public async Task<List<string>> GetFilesAsync()
        {
            return await Task.Run(() => Directory.GetFiles(_baseDirectory).ToList());
        }

        public async Task<bool> PutFileAsync(string fileName, Stream content)
        {
            var filePath = Path.Combine(_baseDirectory, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await content.CopyToAsync(fileStream);
            }
            return true;
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_baseDirectory, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
                return true;
            }
            return false;
        }

        public async Task<Stream> GetFileAsync(string fileName)
        {
            var filePath = Path.Combine(_baseDirectory, fileName);
            if (File.Exists(filePath))
            {
                return await Task.Run(() => File.OpenRead(filePath));
            }
            return null;
        }
    }
}
