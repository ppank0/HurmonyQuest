namespace ContestService.DAL.Models;

public class ParticipantExtendedModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public DateOnly Birthday { get; set; }
    public Guid MusicalInstrumentId { get; set; }
    public required string MusicalInstrumentName { get; set; }
    public Guid NominationId { get; set; }
    public required string NominationName { get; set; }
    public Guid UserId { get; set; }
}
