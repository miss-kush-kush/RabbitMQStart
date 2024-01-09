using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ITSTEPRabbitMQ.Storage.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ITSTEPRabbitMQ.Storage.Storage.Clients
{
    public class AzureStorageClient : IStorageClient
    {
        private readonly BlobContainerClient _containerClient;

        public AzureStorageClient(string connectionString, string containerName)
        {
            _containerClient = new BlobContainerClient(connectionString, containerName);
        }

        public async Task<List<string>> GetFilesAsync()
        {
            var files = new List<string>();
            await foreach (var blob in _containerClient.GetBlobsAsync())
            {
                files.Add(blob.Name);
            }
            return files;
        }

        public async Task<bool> PutFileAsync(string fileName, Stream content)
        {
            var blobClient = _containerClient.GetBlobClient(fileName);
            var response = await blobClient.UploadAsync(content, new BlobHttpHeaders { ContentType = "application/octet-stream" });
            return response != null;
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            var blobClient = _containerClient.GetBlobClient(fileName);
            var response = await blobClient.DeleteIfExistsAsync();
            return response.Value;
        }

        public async Task<Stream> GetFileAsync(string fileName)
        {
            var blobClient = _containerClient.GetBlobClient(fileName);
            var response = await blobClient.DownloadAsync();
            return response.Value.Content;
        }
    }
}
