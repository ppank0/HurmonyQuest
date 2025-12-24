using ApplicationService.BLL.Interfaces;
using ApplicationService.BLL.Models;
using Minio;
using Minio.DataModel.Args;

namespace ApplicationService.BLL.Services
{
    public class MinioStorage(IMinioClient minio) : IVideoStorage
    {
        public async Task DeleteObjectAsync(string bucket, string objName, CancellationToken ct)
        {
            await minio.RemoveObjectAsync(new RemoveObjectArgs()
                .WithBucket(bucket)
                .WithObject(objName), ct);
        }

        private async Task EnsureBucketExists(string bucketName, CancellationToken ct)
        {
            var isExist = await minio.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName), ct);
            if (!isExist) await minio.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName), ct);
        }

        public async Task<FileContentResultModel> GetObjectAsync(string bucket, string objName, CancellationToken ct)
        {
            var ms = new MemoryStream();

            var file = await minio.GetObjectAsync(new GetObjectArgs()
                .WithBucket(bucket)
                .WithObject(objName)
                .WithCallbackStream(stream => stream.CopyTo(ms)), ct);
            ms.Flush();
            ms.Position = 0;
            return new FileContentResultModel(ms, file.ContentType, objName);
        }

        public async Task PutObjectAsync(string bucket, string objName, string contentType, Stream data, CancellationToken ct)
        {
            await EnsureBucketExists(bucket, ct);
            var poa = new PutObjectArgs()
                    .WithBucket(bucket)
                    .WithObject(objName)
                    .WithStreamData(data)
                    .WithObjectSize(data.Length)
                    .WithContentType(contentType);

            await minio.PutObjectAsync(poa, ct);
        }
    }
}
