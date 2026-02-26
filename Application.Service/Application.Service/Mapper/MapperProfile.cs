using Application.Service.Dtos;
using ApplicationService.BLL.Models;
using ApplicationService.BLL.Models.Requests;
using AutoMapper;

namespace Application.Service.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ApplicationDto, ApplicationModel>().ReverseMap();
            CreateMap<CreateApplicationApiRequest, CreateApplicationRequest>()
                .ForCtorParam(nameof(CreateApplicationRequest.VideoId), opt => opt.MapFrom(src => Guid.Empty));
        }
    }
}
