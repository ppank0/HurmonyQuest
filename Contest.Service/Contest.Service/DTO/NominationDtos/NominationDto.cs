using ContestService.API.DTO.MusicalInstrumentDtos;

namespace ContestService.API.DTO.NominationDtos;

public record NominationDto
(
    Guid Id,
    string Name,
    List<MusicalInstrumentDto>? MusicalInstruments
);
