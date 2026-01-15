namespace ApplicationService.BLL.Integrations.Contracts.Participants.DTOs
{
    public sealed record ParticipantCreateRequest(string Name, string Surname, DateOnly Birthday,
        Guid musicalInstrumentId, Guid NominationId);
}
