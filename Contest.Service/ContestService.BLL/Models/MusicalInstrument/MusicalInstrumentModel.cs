namespace ContestService.BLL.Models.MusicalInstrument;

public class MusicalInstrumentModel : ModelBase
{
    public required string Name { get; set; }
    public Guid NominationId { get; set; }
    public string? NominationName { get; set; }
}
