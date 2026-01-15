using ApplicationService.BLL.Models;

namespace ApplicationService.BLL.Interfaces
{
    public interface IVideoService
    {
        Task<FileContentResultModel> GetAsync(string videoUrl, CancellationToken ct);

        Task<VideoModel> PutAsync(string objName, string contentType, Stream data, CancellationToken ct);

        Task DeleteAsync(Guid id, CancellationToken ct);
    }
}
