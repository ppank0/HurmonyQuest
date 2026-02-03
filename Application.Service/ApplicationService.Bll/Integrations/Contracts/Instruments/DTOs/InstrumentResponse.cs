namespace ApplicationService.BLL.Integrations.Contracts.Instruments.DTOs
{
    public record InstrumentResponse(Guid Id, string Name, Guid NominationId);
}
