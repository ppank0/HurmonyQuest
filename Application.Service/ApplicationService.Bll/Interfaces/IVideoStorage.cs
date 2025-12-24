using ApplicationService.BLL.Models;

namespace ApplicationService.BLL.Interfaces
{
    public interface IVideoStorage
    {
        Task<FileContentResultModel> GetObjectAsync(string ObjectStoreName, string objName, CancellationToken ct);
        Task PutObjectAsync(string ObjectStoreName, string objName, string contentType, Stream data, CancellationToken ct);
        Task DeleteObjectAsync(string ObjectStoreName, string objName, CancellationToken ct);
    }
}
