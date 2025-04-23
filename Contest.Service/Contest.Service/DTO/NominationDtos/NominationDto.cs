using ContestService.API.DTO.MusicalInstrumentDtos;
using ContestService.BLL.Models;

namespace ContestService.API.DTO.NominationDtos;

public record NominationDto
(
    Guid Id,
    string Name,
    List<MusicalInstrumentDto>? musicalInstrumentDtos
);
