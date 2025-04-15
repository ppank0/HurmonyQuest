using System.ComponentModel.DataAnnotations;

namespace ContestService.DAL.Entities;

public class Jury : BaseEntity
{
    public required string Name { get; set; } 
    public required string Surname { get; set; } 
    public DateOnly Birthday { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public Guid UserId { get; set; }
}
