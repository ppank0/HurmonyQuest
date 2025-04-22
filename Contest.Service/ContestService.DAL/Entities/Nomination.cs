namespace ContestService.DAL.Entities;

public class Nomination : BaseEntity
{
    public required string Name { get; set; }
    public ICollection<MusicalInstrument>? Instruments { get; set; }
}
