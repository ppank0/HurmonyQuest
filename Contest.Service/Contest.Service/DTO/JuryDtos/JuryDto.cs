namespace ContestService.API.DTO.JuryDtos;

public record JuryDto
(
    Guid Id,
    string Name,
    string Surname,
    DateOnly Birthday,
    Guid UserId
);
