namespace ContestService.BLL.Models;

public class StageModel : ModelBase
{
    public required string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
