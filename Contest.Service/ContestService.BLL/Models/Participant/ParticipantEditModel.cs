using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestService.BLL.Models.Participant;

public class ParticipantEditModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateOnly Birthday { get; set; }
    public Guid MusicalInstrumentId { get; set; }
    public Guid NominationId { get; set; }
}
