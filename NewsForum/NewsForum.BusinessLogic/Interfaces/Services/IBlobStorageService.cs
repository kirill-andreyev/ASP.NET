using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace NewsForum.BusinessLogic.Interfaces.Services
{
    public interface IBlobStorageService
    {
        public Task UploadToBlobStorage(Stream fileStream, string fileName);
        public Task DeleteBlob(string blobName);
        public Task<string> GetBlob(string blobName);
    }
}
