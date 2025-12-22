using ApplicationService.BLL.Integrations.Contracts.Participants.DTOs;
using ApplicationService.BLL.Models;
using ApplicationService.BLL.Models.Requests;
using ApplicationService.DAL.Entities;
using AutoMapper;

namespace ApplicationService.BLL.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<VideoEntity, VideoModel>().ReverseMap();
            CreateMap<ApplicationEntity, ApplicationModel>().ReverseMap();
            CreateMap<CreateApplicationRequest, ParticipantCreateRequest>().ReverseMap();
        }
    }
}
