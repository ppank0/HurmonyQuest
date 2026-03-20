using ContestService.DAL.Models;

namespace ContestService.DAL.Repositories.Interfaces;

public interface IParticipantRepository
{
    Task<List<ParticipantExtendedModel>> GetAllWithDetails(CancellationToken ct);
}
