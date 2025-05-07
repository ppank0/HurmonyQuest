namespace ContestService.API.DTO.MusicalInstrumentDtos;

public record MusicalInstrumentEditDto
(
    Guid Id,
    string Name,
    Guid NominationId
);
