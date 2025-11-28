using ApplicationService.BLL.Interfaces;
using ApplicationService.BLL.Models;
using ApplicationService.DAL.Entities;
using ApplicationService.DAL.UnitOfWork;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using static ApplicationService.BLL.Consts.MinIOConsts;

namespace ApplicationService.BLL.Services
{
    public class VideoService(IUnitOfWork uOw, IMapper mapper, IVideoStorage storage) : IVideoService
    {
        private readonly IUnitOfWork _uOw = uOw;
        private readonly IMapper _mapper = mapper;
        private readonly IVideoStorage _videoStorage = storage;

        public Task<List<VideoModel>> GetAllAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<(Stream, string)> GetAsync(string videoName, CancellationToken ct)
        {
            return await _videoStorage.GetObjectAsync(BucketName, videoName, ct);
        }

        public async Task<VideoModel> PutAsync(string objName, string contentType, Stream data, CancellationToken ct)
        {
            await _videoStorage.EnsureBucketExists(BucketName, ct);
            await _videoStorage.PutObjectAsync(BucketName, objName, contentType, data, ct);
            var videoModel = new VideoModel() { VideoUrl = objName };
            var video = await _uOw.Videos.CreateAsync(_mapper.Map<VideoEntity>(videoModel), ct);

            await _uOw.SaveAsync(ct);

            return _mapper.Map<VideoModel>(video);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var videoModelList = _uOw.Videos.FindByCondition((s => s.Id == id), ct);
            var videoModel = await videoModelList.FirstOrDefaultAsync();
            if (videoModel is null)
            {
                throw new NullReferenceException();
            }

            await _videoStorage.DeleteObjectAsync(BucketName, videoModel.VideoUrl, ct);
            await _uOw.Videos.DeleteAsync((_mapper.Map<VideoEntity>(videoModel)), ct);

            await _uOw.SaveAsync(ct);
        }
    }
}
