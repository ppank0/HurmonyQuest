namespace ContestService.DAL.Models;

public class MusicalInstrumentExtendedModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid NominationId { get; set; }
    public string NominationName { get; set; }
}
