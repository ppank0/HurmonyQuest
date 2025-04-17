using ContestService.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestService.BLL.Models;
public class ParticipantModel : ModelBase
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public DateOnly Birthday { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

    [ForeignKey("MusicalInstrument")]
    public Guid MusicalInstrumentId { get; set; }
    public MusicalInstrument MusicalInstrument { get; set; }
    public Guid NominationId { get; set; }
    public Guid UserId { get; set; }
}
