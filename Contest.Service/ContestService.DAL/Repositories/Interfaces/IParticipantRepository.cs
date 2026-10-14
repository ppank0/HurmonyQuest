using ContestService.DAL.Entities;

namespace ContestService.DAL.Repositories.Interfaces;

public interface IParticipantRepository : IRepositoryBase<Participant>
{
    Task<List<Participant>> GetAllWithDetails(CancellationToken ct);
    Task<Participant?> GetByIdWithDetails(Guid id, CancellationToken ct);
}
