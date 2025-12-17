namespace ApplicationService.BLL.Interfaces
{
    public interface IVideoStorage
    {
        Task<(Stream, string)> GetObjectAsync(string bucket, string objName, CancellationToken ct);
        Task PutObjectAsync(string bucket, string objName, string contentType, Stream data, CancellationToken ct);

        Task EnsureBucketExists(string bucketName, CancellationToken ct);

        Task DeleteObjectAsync(string bucket, string objName, CancellationToken ct);
    }
}
