namespace ContestService.API.DTO.MusicalInstrumentDtos;

public record MusicalInstrumentDto
(
    Guid Id,
    string Name,
    Guid NominationId
);
