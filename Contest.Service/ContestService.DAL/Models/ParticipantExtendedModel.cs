namespace ContestService.DAL.Models;

public class ParticipantExtendedModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateOnly Birthday { get; set; }
    public Guid MusicalInstrumentId { get; set; }
    public string MusicalInstrumentName { get; set; }
    public Guid NominationId { get; set; }
    public string NominationName { get; set; }
    public Guid UserId { get; set; }
}
