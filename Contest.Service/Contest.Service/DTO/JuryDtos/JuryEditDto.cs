using System.Text.Json.Serialization;

namespace ContestService.API.DTO.JuryDtos;

public record JuryEditDto
(
    string Name,
    string Surname,
    DateOnly Birthday 
);
