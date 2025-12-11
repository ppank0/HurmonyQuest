using ApplicationService.BLL.Exeptions;
using ApplicationService.BLL.Interfaces;
using ApplicationService.BLL.Models;
using ApplicationService.DAL.Entities;
using ApplicationService.DAL.UnitOfWork;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using static ApplicationService.BLL.Consts.MinIOConsts;

namespace ApplicationService.BLL.Services
{
    public class VideoService(IUnitOfWork uOw, IMapper mapper, IVideoStorage videoStorage) : IVideoService
    {
        public Task<List<VideoModel>> GetAllAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<(Stream, string)> GetAsync(string videoName, CancellationToken ct)
        {
            return await videoStorage.GetObjectAsync(BucketName, videoName, ct);
        }

        public async Task<VideoModel> PutAsync(string objName, string contentType, Stream data, CancellationToken ct)
        {
            await videoStorage.EnsureBucketExists(BucketName, ct);
            await videoStorage.PutObjectAsync(BucketName, objName, contentType, data, ct);
            var videoModel = new VideoModel() { VideoUrl = objName };
            var video = await uOw.Videos.CreateAsync(mapper.Map<VideoEntity>(videoModel), ct);

            await uOw.SaveAsync(ct);

            return mapper.Map<VideoModel>(video);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var videoModelList = uOw.Videos.FindByCondition((s => s.Id == id), ct);
            var videoModel = await videoModelList.FirstOrDefaultAsync();
            if (videoModel is null)
            {
                throw new NotFoundException($"Video with this id: {id} was not found");
            }

            await videoStorage.DeleteObjectAsync(BucketName, videoModel.VideoUrl, ct);
            await uOw.Videos.DeleteAsync((mapper.Map<VideoEntity>(videoModel)), ct);

            await uOw.SaveAsync(ct);
        }
    }
}
