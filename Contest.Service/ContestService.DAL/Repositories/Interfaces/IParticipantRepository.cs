using ContestService.DAL.Entities;
using ContestService.DAL.Models;

namespace ContestService.DAL.Repositories.Interfaces;

public interface IParticipantRepository : IRepositoryBase<Participant>
{
    Task<List<ParticipantExtendedModel>> GetAllWithDetails(CancellationToken ct);
}
