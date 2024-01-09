using ITSTEPRabbitMQ.Storage.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ITSTEPRabbitMQ.Storage
{
    public class StorageClass : IStorageClient
    {
        private readonly IStorageClient _client;

        public StorageClass(IStorageClient client)
        {
            _client = client;
        }

        public Task<List<string>> GetFilesAsync()
        {
            return _client.GetFilesAsync();
        }

        public Task<bool> PutFileAsync(string fileName, Stream content)
        {
            return _client.PutFileAsync(fileName, content);
        }

        public Task<bool> DeleteFileAsync(string fileName)
        {
            return _client.DeleteFileAsync(fileName);
        }

        public Task<Stream> GetFileAsync(string fileName)
        {
            return _client.GetFileAsync(fileName);
        }
    }
}
