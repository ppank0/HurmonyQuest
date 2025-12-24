using ApplicationService.BLL.Interfaces;
using ApplicationService.BLL.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace ApplicationService.BLL.Services
{
    public class BlobStorage(BlobServiceClient blobServiceClient) : IVideoStorage
    {
        public async Task DeleteObjectAsync(string containerName, string objName, CancellationToken ct)
        {
            var blobContainer = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainer.GetBlobClient(objName);
            await blobClient.DeleteIfExistsAsync(cancellationToken: ct);
        }

        public async Task<FileContentResultModel> GetObjectAsync(string containerName, string objName, CancellationToken ct)
        {
            var blobContainer = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainer.GetBlobClient(objName);
            BlobProperties props = await blobClient.GetPropertiesAsync(cancellationToken: ct);
            var stream = await blobClient.OpenReadAsync(cancellationToken: ct);

            return new FileContentResultModel(stream, props.ContentType, objName); ;
        }

        public async Task PutObjectAsync(string containerName, string objName, string contentType, Stream data, CancellationToken ct)
        {
            var blobContainer = blobServiceClient.GetBlobContainerClient(containerName);
            await blobContainer.CreateIfNotExistsAsync(cancellationToken: ct);
            var blobClient = blobContainer.GetBlobClient(objName);
            await blobClient.UploadAsync(
                data,
                new BlobHttpHeaders { ContentType = contentType },
                cancellationToken: ct);
        }
    }
}
