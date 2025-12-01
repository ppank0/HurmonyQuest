using ContestService.DAL.Entities;

namespace ContestService.DAL.Repositories.Interfaces;
public interface IParticipantRepository : IRepositoryBase<Participant>
{
    Task<List<Participant>> GetAllWithRelationsAsync(CancellationToken cancellationToken);
}
