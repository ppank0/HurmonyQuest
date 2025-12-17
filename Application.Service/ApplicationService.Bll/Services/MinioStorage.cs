using ApplicationService.BLL.Interfaces;
using Minio;
using Minio.DataModel.Args;

namespace ApplicationService.BLL.Services
{
    public class MinioStorage(IMinioClient minio) : IVideoStorage
    {
        private readonly IMinioClient _minio = minio;
        public async Task DeleteObjectAsync(string bucket, string objName, CancellationToken ct)
        {
            await _minio.RemoveObjectAsync(new RemoveObjectArgs()
                .WithBucket(bucket)
                .WithObject(objName), ct);
        }

        public async Task EnsureBucketExists(string bucketName, CancellationToken ct)
        {
            var isExist = await _minio.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName), ct);
            if (!isExist) await _minio.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName), ct);
        }

        public async Task<(Stream, string)> GetObjectAsync(string bucket, string objName, CancellationToken ct)
        {
            var ms = new MemoryStream();

            var file = await _minio.GetObjectAsync(new GetObjectArgs()
                .WithBucket(bucket)
                .WithObject(objName)
                .WithCallbackStream(stream => stream.CopyTo(ms)), ct);

            return (ms, file.ContentType);
        }

        public async Task PutObjectAsync(string bucket, string objName, string contentType, Stream data, CancellationToken ct)
        {
            var poa = new PutObjectArgs()
                    .WithBucket(bucket)
                    .WithObject(objName)
                    .WithStreamData(data)
                    .WithObjectSize(data.Length)
                    .WithContentType(contentType);

            await _minio.PutObjectAsync(poa, ct);
        }
    }
}
