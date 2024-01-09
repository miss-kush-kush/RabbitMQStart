namespace ITSTEPRabbitMQ.Storage.Interfaces
{
    public interface IStorageClient
    {
        Task<List<string>> GetFilesAsync();
        Task<bool> PutFileAsync(string fileName, Stream content);
        Task<bool> DeleteFileAsync(string fileName);
        Task<Stream> GetFileAsync(string fileName);
    }
}
