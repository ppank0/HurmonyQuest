namespace ContestService.API.DTO.ParticipantDtos;

public record ParticipantDto
(
    Guid Id,
    string Name,
    string Surname,
    DateOnly Birthday,
    Guid MusicalInstrumentId,
    Guid NominationId,
    Guid UserId
);
