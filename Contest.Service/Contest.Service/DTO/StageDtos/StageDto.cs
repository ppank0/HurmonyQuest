namespace ContestService.API.DTO.StageDtos;

public record StageDto
(
    Guid Id,
    string Name,
    DateTime StartDate,
    DateTime EndDate
);
