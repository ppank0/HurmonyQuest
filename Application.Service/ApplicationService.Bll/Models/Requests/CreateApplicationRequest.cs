namespace ApplicationService.BLL.Models.Requests
{
    public record CreateApplicationRequest(string Name,
    string Surname,
    DateOnly Birthday,
    Guid MusicalInstrumentId,
    Guid NominationId,
    Guid VideoId);
}
