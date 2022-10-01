namespace NewsForum.BusinessLogic.Interfaces.Services
{
    public interface IBlobStorageService
    {
        public Task UploadToBlobStorage(Stream fileStream, string fileName);
        public Task DeleteBlob(string blobName);
        public Task<string> GetBlob(string blobName);
    }
}