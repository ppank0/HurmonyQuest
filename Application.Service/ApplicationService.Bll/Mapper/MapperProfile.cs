using ApplicationService.BLL.Models;
using ApplicationService.BLL.Models.ApplicationModel;
using ApplicationService.DAL.Entities;
using AutoMapper;

namespace ApplicationService.BLL.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ApplicationEntity, ApplicationModel>().ReverseMap();
            CreateMap<VideoEntity, VideoModel>().ReverseMap();
        }
    }
}
