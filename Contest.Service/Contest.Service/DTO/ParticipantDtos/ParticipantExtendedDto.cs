namespace ContestService.API.DTO.ParticipantDtos;

public record ParticipantExtendedDto
(
    Guid Id,
    string Name,
    string Surname,
    DateOnly Birthday,
    Guid MusicalInstrumentId,
    string MusicalInstrumentName,
    Guid NominationId, string NominationName,
    Guid UserId
);
