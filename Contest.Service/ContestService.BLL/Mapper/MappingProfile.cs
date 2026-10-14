using AutoMapper;
using ContestService.BLL.Models;
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
            .ForMember(x => x.NominationName, opt => opt.MapFrom(scr => scr.MusicalInstrument!.Nomination != null ?
                scr.MusicalInstrument.Nomination.Name : ""));
        CreateMap<ParticipantModel, Participant>()
            .ForMember(x => x.MusicalInstrument, opt => opt.Ignore());

        CreateMap<Stage, StageModel>().ReverseMap();
        CreateMap<Nomination, NominationModel>()
            .ForMember(x => x.MusicalInstruments, opt => opt.MapFrom(src => src.MusicalInstruments));

        CreateMap<MusicalInstrument, MusicalInstrumentModel>()
            .ForMember(x => x.NominationName, opt => opt.MapFrom(src => src.Nomination != null ? src.Nomination.Name : ""));
        CreateMap<MusicalInstrumentModel,MusicalInstrument>()
            .ForMember(x => x.Nomination, opt => opt.Ignore());
    }
}
