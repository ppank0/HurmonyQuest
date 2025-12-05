using ApplicationService.BLL.Models;
using ApplicationService.DAL.Entities;
using AutoMapper;

namespace ApplicationService.BLL.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<VideoEntity, VideoModel>().ReverseMap();
        }
    }
}
