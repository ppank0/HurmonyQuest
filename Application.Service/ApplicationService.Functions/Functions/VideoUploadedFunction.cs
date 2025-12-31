using ApplicationService.Functions.Models;
using ApplicationService.Functions.Services;
using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ApplicationService.Functions.Functions
{
    public class VideoUploadedFunction
    {
        private readonly ILogger<VideoUploadedFunction> _logger;
        private readonly IVideoMetadataTableService _service;

        public VideoUploadedFunction(ILogger<VideoUploadedFunction> logger, IVideoMetadataTableService service)
        {
            _logger = logger;
            _service = service;
        }

        [Function(nameof(VideoUploadedFunction))]
        public async Task Run([BlobTrigger("videoblobs/{name}", Connection = "AzureWebJobsStorage")] BlobClient blobClient,
                                                                                        string name, CancellationToken ct)
        {
            var props = await blobClient.GetPropertiesAsync(cancellationToken: ct);

            _logger.LogInformation(
                "Blob processed. Name={Name}, Size={SizeBytes}, Container={Container}, CreatedOn={CreatedOn}",
                name,
                props.Value.ContentLength,
                blobClient.BlobContainerName,
                props.Value.CreatedOn
            );

            var entity = new VideoMetadataEntity
            {
                RowKey = name,
                BlobName = name,
                Size = props.Value.ContentLength,
                ContentType = props.Value.ContentType ?? "",
                ContentEncoding = props.Value.ContentEncoding,
                CreatedOn = props.Value.CreatedOn,
            };

            await _service.UpsertAsync(entity, ct);
        }
    }
}
