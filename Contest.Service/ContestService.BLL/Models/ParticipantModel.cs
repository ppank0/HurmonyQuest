using ContestService.DAL.Entities;

namespace ContestService.BLL.Models;

public class ParticipantModel : ModelBase
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public DateOnly Birthday { get; set; }
    public Guid MusicalInstrumentId { get; set; }
    public string MusicalInstrumentName { get; set; }
    public Guid NominationId { get; set; }
    public string NominationName {  get; set; }
    public Guid UserId { get; set; }
}
