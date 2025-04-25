namespace ContestService.API.DTO.ParticipantDtos;

public record ParticipantEditDto
(
    string Name,
    string Surname,
    DateOnly Birthday,
    Guid MusicalInstrumentId,
    Guid NominationId
);
