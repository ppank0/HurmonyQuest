using ApplicationService.Functions.Models;
using Azure.Data.Tables;
using Azure.Storage.Blobs.Models;

namespace ApplicationService.Functions.Services
{
    public class VideoMetadataTableService(TableServiceClient tableService) : IVideoMetadataTableService
    {
        private static readonly string tableName = "videotable";

        public async Task UpsertAsync(VideoMetadataEntity entity, CancellationToken ct)
        {
            await tableService.CreateTableIfNotExistsAsync(tableName, ct);
            var tableClient = tableService.GetTableClient(tableName);

            await tableClient.UpsertEntityAsync(entity);
        }
    }
}
