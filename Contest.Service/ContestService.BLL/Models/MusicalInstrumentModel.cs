namespace ContestService.BLL.Models;

public class MusicalInstrumentModel : ModelBase
{
    public required string Name { get; set; }
    public Guid NominationId { get; set; }
    public string NominationName { get; set; } = String.Empty;
}
