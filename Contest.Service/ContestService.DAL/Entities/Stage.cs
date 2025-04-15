namespace ContestService.DAL.Entities;

public class Stage : BaseEntity
{
    public required string Name { get; set; }
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime EndDate { get; set; } = DateTime.UtcNow;
}
