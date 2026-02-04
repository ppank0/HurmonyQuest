namespace ApplicationService.BLL.Integrations.Contracts.Participants.DTOs
{
    public sealed record ParticipantResponse(Guid Id, string Name, string Surname, DateOnly Birthday, Guid sub);
}
