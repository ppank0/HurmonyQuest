namespace ContestService.BLL.Models;

public class JuryModel : ModelBase
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public DateOnly Birthday { get; set; }
    public Guid UserId { get; set; }
}
