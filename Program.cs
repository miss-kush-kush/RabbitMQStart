using Azure.Storage.Blobs;
using ITSTEPRabbitMQ.Storage.Clients;
using ITSTEPRabbitMQ.Storage.Storage.Clients;
using ITSTEPRabbitMQ.Storage;
using ITSTEPRabbitMQ.Storage.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Define connection settings and container name
        string connectionString = "DefaultEndpointsProtocol=https;AccountName=pv121azure;AccountKey=7kdrUTO1rzU0e3DVXn3ggsuLtMoWxoMByPIlJ9vIwN6z8FPmuON/Sb7nTcFr+srqvp6htysHbRHN+AStybU1yg==;EndpointSuffix=core.windows.net";
        string containerName = "avatarpics";

        // Define the storage type (1 for Local, 2 for Azure)
        string storageType = "2"; // Example: Using Azure Storage

        StorageClass storage = storageType switch
        {
            "1" => new StorageClass(new LocalStorageClient("D:/Programming/avatarpics")),
            "2" => new StorageClass(new AzureStorageClient(connectionString, containerName)),
            _ => throw new InvalidOperationException("Invalid storage type")
        };

        // Define file path for upload
        string filePath = "avatarpics/dog.jpg";

        // Upload file
        try
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                await storage.PutFileAsync(Path.GetFileName(filePath), fileStream);
                Console.WriteLine("Файл загружен.");
            }
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine($"Directory not found: {ex.Message}");
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"File not found: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        // List files
        var files = await storage.GetFilesAsync();
        foreach (var file in files)
        {
            Console.WriteLine(file);
        }

        // Get file
        string fileName = "photo_2023-04-25_21-41-24.jpg"; 
        using (var fileStream = await storage.GetFileAsync(fileName))
        {
            if (fileStream != null)
            {
                Console.WriteLine($"Файл {fileName} получен.");
               
            }
            else
            {
                Console.WriteLine("Файл не найден.");
            }
        }

        //Delete file
        string fileToDelete = "photo_2023-04-25_21-41-24.jpg";
        var result = await storage.DeleteFileAsync(fileToDelete);
        Console.WriteLine(result ? "Файл удален." : "Файл не найден.");
    }
}
