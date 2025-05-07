namespace ContestService.API.DTO.StageDtos;

public record StageEditDto
(
    string Name,
    DateTime StartDate,
    DateTime EndDate
);
