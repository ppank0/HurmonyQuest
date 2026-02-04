<<<<<<< feature/9-create-integration-tests
﻿using ApplicationService.BLL.Models;
=======
﻿using ApplicationService.BLL.Integrations.Contracts.Participants.DTOs;
using ApplicationService.BLL.Models;
using ApplicationService.BLL.Models.Requests;
>>>>>>> main
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
<<<<<<< feature/9-create-integration-tests
=======
            CreateMap<CreateApplicationRequest, ParticipantCreateRequest>().ReverseMap();
>>>>>>> main
        }
    }
}
