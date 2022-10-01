using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
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
            var blobServiceClient =
                new BlobServiceClient(configuration.GetConnectionString("BlobStorageConnection"));
            _blobContainerClient = blobServiceClient.GetBlobContainerClient("newsforum");
            _blobContainerClient.CreateIfNotExists();
            _blobContainerClient.SetAccessPolicy(PublicAccessType.BlobContainer);

        }


        public async Task UploadToBlobStorage(Stream fileStream, string fileName)
        {
            var blobClient = _blobContainerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileStream, true);
            fileStream.Close();
        }

        public async Task DeleteBlob(string blobName)
        {
            var blobClient = _blobContainerClient.GetBlobClient(blobName);
            await blobClient.DeleteAsync();
        }

        public async Task<string> GetBlob(string blobName)
        {
            var blobClient = _blobContainerClient.GetBlobClient(blobName);
            var blobUrl = blobClient.Uri.AbsoluteUri;
            return blobUrl;
        }
    }
}