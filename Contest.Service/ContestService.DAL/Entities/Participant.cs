using System.ComponentModel.DataAnnotations.Schema;

namespace ContestService.DAL.Entities;

public class Participant : BaseEntity
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public DateOnly Birthday { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

    [ForeignKey("MusicalInstrument")]
    public Guid MusicalInstrumentId { get; set; }
    public MusicalInstrument? MusicalInstrument { get; set; }
    public Guid UserId { get; set; }


}
