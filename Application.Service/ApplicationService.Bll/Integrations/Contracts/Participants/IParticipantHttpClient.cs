using ApplicationService.BLL.Integrations.Contracts.Participants.DTOs;

namespace ApplicationService.BLL.Integrations.Contracts.Participant
{
    public interface IParticipantHttpClient
    {
        Task<ParticipantResponse> GetAsync(Guid id, CancellationToken ct);
        Task<ParticipantResponse> CreateAsync(ParticipantCreateRequest req, CancellationToken ct);
        Task DeleteAsync(Guid id, CancellationToken ct);
    }
}
