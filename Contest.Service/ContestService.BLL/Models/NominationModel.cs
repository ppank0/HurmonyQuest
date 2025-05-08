namespace ContestService.BLL.Models;

public class NominationModel : ModelBase
{
    public required string Name { get; set; }
    public List<MusicalInstrumentModel>? MusicalInstruments { get; set; }
}
