using AutoMapper;
using ContestService.BLL.Models;
using ContestService.DAL.Entities;
using ContestService.DAL.Models;

namespace ContestService.BLL.Mapper;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Jury, JuryModel>().ReverseMap();

        CreateMap<Participant, ParticipantModel>();
        CreateMap<ParticipantExtendedModel, ParticipantModel>();
        CreateMap<ParticipantModel, Participant>()
            .ForMember(x => x.MusicalInstrument, opt => opt.Ignore());

        CreateMap<Stage, StageModel>().ReverseMap();
        CreateMap<Nomination, NominationModel>().ReverseMap();

        CreateMap<MusicalInstrument, MusicalInstrumentModel>()
            .ForMember(x => x.NominationName, opt => opt.Ignore());
        CreateMap<MusicalInstrumentModel,MusicalInstrument>()
            .ForMember(x => x.Nomination, opt => opt.Ignore());
        CreateMap<MusicalInstrumentExtendedModel, MusicalInstrumentModel>().ReverseMap();
    }
}
