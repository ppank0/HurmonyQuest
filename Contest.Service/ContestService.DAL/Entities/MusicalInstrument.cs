using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContestService.DAL.Entities;

public class MusicalInstrument : BaseEntity
{
    public required string Name { get; set; } 

    [ForeignKey("Nomination")]
    public Guid NominationId { get; set; }
    public Nomination? Nomination { get; set; }
}
