using Azure;
using Azure.Data.Tables;
using System.Reflection.Metadata;

namespace ApplicationService.Functions.Models
{
    public class VideoMetadataEntity : ITableEntity
    {
        public string PartitionKey { get; set; } = "videoblobs";
        public string RowKey { get; set; } = default!;
        public string BlobName { get; set; } = string.Empty;
        public long Size { get; set; }
        public string ContentType { get; set; }
        public string ContentEncoding { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public ETag ETag { get; set; } = ETag.All;
        public DateTimeOffset? Timestamp { get; set; }
    }
}
