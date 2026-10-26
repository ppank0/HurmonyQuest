using AutoMapper;
using ContestService.BLL.Models;
using ContestService.BLL.Models.MusicalInstrument;
using ContestService.BLL.Models.Participant;
using ContestService.DAL.Entities;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace ContestService.BLL.Mapper;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Jury, JuryModel>().ReverseMap();

        CreateMap<Participant, ParticipantModel>()
            .ForMember(x => x.NominationId, opt => opt.MapFrom(src => src.MusicalInstrument!.NominationId))
            .ForMember(x => x.NominationName, opt => opt.MapFrom(src => src.MusicalInstrument != null ? 
                (src.MusicalInstrument.Nomination != null ? src.MusicalInstrument.Nomination.Name : string.Empty)
                    : string.Empty))
            .ForMember(x => x.MusicalInstrumentName, opt => opt.NullSubstitute(string.Empty));
        CreateMap<ParticipantModel, Participant>()
            .ForMember(x => x.MusicalInstrument, opt => opt.Ignore());
        CreateMap<ParticipantEditModel, Participant>();

        CreateMap<Stage, StageModel>().ReverseMap();
        CreateMap<Nomination, NominationModel>()
            .ForMember(x => x.MusicalInstruments, opt => opt.MapFrom(src => src.MusicalInstruments));
        CreateMap<NominationModel, Nomination>();

        CreateMap<MusicalInstrument, MusicalInstrumentModel>()
            .ForMember(x => x.NominationName, opt => opt.MapFrom(src => src.Nomination != null ? src.Nomination.Name : ""));
        CreateMap<MusicalInstrumentModel,MusicalInstrument>()
            .ForMember(x => x.Nomination, opt => opt.Ignore());
        CreateMap<MusicalInstrumentEditModel, MusicalInstrument>();
    }
}
