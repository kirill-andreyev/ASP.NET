using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using NewsForum.BusinessLogic.Interfaces.Services;

namespace NewsForum.BusinessLogic.Implementations.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        public readonly BlobContainerClient _blobContainerClient;

        public BlobStorageService()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            BlobServiceClient blobServiceClient =
                new BlobServiceClient(configuration.GetConnectionString("BlobStorageConnection"));
            _blobContainerClient = blobServiceClient.GetBlobContainerClient("newsforum");
        }


        public async Task UploadToBlobStorage(Stream fileStream, string fileName)
        {
            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileStream, true);
            fileStream.Close();
        }

        public async Task DeleteBlob(string blobName)
        {
            BlobClient blobClient = _blobContainerClient.GetBlobClient(blobName);
            await blobClient.DeleteAsync();
        }

        public async Task<string> GetBlob(string blobName)
        {
            BlobClient blobClient = _blobContainerClient.GetBlobClient(blobName);
            var blobUrl = blobClient.Uri.AbsoluteUri;
            return blobUrl;
        }
    }
}
