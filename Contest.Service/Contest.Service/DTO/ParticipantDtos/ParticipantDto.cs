using ContestService.API.DTO.MusicalInstrumentDtos;

namespace ContestService.API.DTO.ParticipantDtos;

public record ParticipantDto
(
    Guid Id,
    string Name,
    string Surname,
    DateOnly Birthday,
    Guid MusicalInstrumentId,
    string MusicalInstrumentName,
    Guid NominationId
);
