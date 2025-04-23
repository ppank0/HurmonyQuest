using ContestService.API.DTO.MusicalInstrumentDtos;
using ContestService.BLL.Models;

namespace ContestService.API.DTO.ParticipantDtos;

public record ParticipantDto
(
    Guid Id,
    string Name,
    string Surname,
    DateOnly Birthday,
    Guid MusicalInstrumentId,
    MusicalInstrumentDto? MusicalInstrument,
    Guid NominationId
);
