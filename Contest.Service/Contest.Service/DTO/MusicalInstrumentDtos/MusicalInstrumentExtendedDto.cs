namespace ContestService.API.DTO.MusicalInstrumentDtos;

public record MusicalInstrumentExtendedDto(
    Guid Id,
    string Name,
    Guid NominationId,
    string NominationName
);
