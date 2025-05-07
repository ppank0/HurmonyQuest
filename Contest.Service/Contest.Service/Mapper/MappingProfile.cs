using AutoMapper;
using ContestService.API.DTO.JuryDtos;
using ContestService.API.DTO.MusicalInstrumentDtos;
using ContestService.API.DTO.NominationDtos;
using ContestService.API.DTO.ParticipantDtos;
using ContestService.API.DTO.StageDtos;
using ContestService.BLL.Models;

namespace ContestService.API.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<NominationDto, NominationModel>().ReverseMap();
        CreateMap<NominationEditDto, NominationModel>();

        CreateMap<JuryDto, JuryModel>().ReverseMap();
        CreateMap<JuryModel, JuryEditDto>().ReverseMap();

        CreateMap<StageDto, StageModel>().ReverseMap();
        CreateMap<StageEditDto, StageModel>();

        CreateMap<ParticipantDto, ParticipantModel>().ReverseMap();
        CreateMap<ParticipantEditDto, ParticipantModel>();

        CreateMap<MusicalInstrumentDto, MusicalInstrumentModel>().ReverseMap();
        CreateMap<MusicalInstrumentEditDto, MusicalInstrumentModel>();  
    }
}
