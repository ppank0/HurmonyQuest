using ApplicationService.Functions.Models;
using Azure.Storage.Blobs.Models;

namespace ApplicationService.Functions.Services
{
    public interface IVideoMetadataTableService
    {
        Task UpsertAsync(VideoMetadataEntity entity, CancellationToken ct);
    }
}
