using AutoMapper;
using ContestService.BLL.Models;
using ContestService.DAL.Entities;

namespace ContestService.BLL.Mapper;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Jury, JuryModel>().ReverseMap();
        CreateMap<Participant, ParticipantModel>().ReverseMap();
        CreateMap<Stage, StageModel>().ReverseMap();
        CreateMap<Nomination, NominationModel>().ReverseMap();
        CreateMap<MusicalInstrument, MusicalInstrumentModel>().ReverseMap();
    }
}
