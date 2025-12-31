using ApplicationService.BLL.Exeptions;
using ApplicationService.BLL.Interfaces;
using ApplicationService.BLL.Models;
using ApplicationService.DAL.Entities;
using ApplicationService.DAL.UnitOfWork;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using static ApplicationService.BLL.Consts.AzureBlobConsts;

namespace ApplicationService.BLL.Services
{
    public class VideoService(IUnitOfWork unitOfWork, IMapper mapper, IVideoStorage videoStorage) : IVideoService
    {
        public async Task<FileContentResultModel> GetAsync(string videoName, CancellationToken ct)
        {
            return await videoStorage.GetObjectAsync(ObjectStoreName, videoName, ct);
        }

        public async Task<VideoModel> PutAsync(string objName, string contentType, Stream data, CancellationToken ct)
        {
            objName = $"{Guid.NewGuid()}{objName}";
            await videoStorage.PutObjectAsync(ObjectStoreName, objName, contentType, data, ct);
            var videoEntity = new VideoEntity() { VideoUrl = objName };
            await unitOfWork.Videos.CreateAsync(videoEntity, ct);

            await unitOfWork.SaveAsync(ct);

            return mapper.Map<VideoModel>(videoEntity);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var videoModelList = unitOfWork.Videos.FindByCondition((s => s.Id == id), ct);
            var videoModel = await videoModelList.FirstOrDefaultAsync();
            if (videoModel is null)
            {
                throw new NotFoundException($"Video with this id: {id} was not found");
            }

            await videoStorage.DeleteObjectAsync(ObjectStoreName, videoModel.VideoUrl, ct);
            await unitOfWork.Videos.DeleteAsync((mapper.Map<VideoEntity>(videoModel)), ct);

            await unitOfWork.SaveAsync(ct);
        }
    }
}
