using Azure.Storage.Blobs;

namespace HeroesAndVillains.Infrastructure.Repositories
{
    public class AzureBlobStore
    {
        private BlobServiceClient _blobServiceClient;
        private BlobContainerClient _blobContainerClient;

        public AzureBlobStore(string nameOrConnectionString, string containerName) 
        {
            _blobServiceClient = new BlobServiceClient(nameOrConnectionString);
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        }

        public async Task<AzureBlobStore> CreateContainerIfNotExist()
        {
            await _blobContainerClient.CreateIfNotExistsAsync();
            return this;
        }

        public async Task SaveImageInBlobAsync(Guid recordId, string imageName, byte[] imageContent) 
        {
            var blobClient = _blobContainerClient.GetBlobClient(PathToImage(recordId, imageName));

            using (MemoryStream memoryStream = new MemoryStream(imageContent)) 
            {
                await blobClient.UploadAsync(memoryStream, true);
            }
        }

        public async Task<byte[]?> RetrieveImageFromBlobAsync(Guid recordId, string imageName)
        {
            var blobClient = _blobContainerClient.GetBlobClient(PathToImage(recordId, imageName));
            var blobProperties = await blobClient.GetPropertiesAsync();

            if (blobProperties != null && blobProperties.Value.ContentLength > 0) 
            {
                using MemoryStream ms = new MemoryStream();
                await blobClient.DownloadToAsync(ms);
                return ms.ToArray();
            }

            return null;
        }

        private string PathToImage(Guid recordId, string fileName) => $"{recordId}/{fileName}";
    }
}
