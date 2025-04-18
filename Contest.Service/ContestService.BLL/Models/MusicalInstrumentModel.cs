using ContestService.DAL.Entities;

namespace ContestService.BLL.Models;

public class MusicalInstrumentModel : ModelBase
{
    public required string Name { get; set; }
    public Guid NominationId { get; set; }
    public NominationModel Nomination { get; set; }
}
