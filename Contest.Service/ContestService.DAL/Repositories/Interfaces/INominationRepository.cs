using ContestService.DAL.Entities;

namespace ContestService.DAL.Repositories.Interfaces;
public interface INominationRepository : IRepositoryBase<Nomination>
{
    bool IsMusicalInstrumentInNomination(Guid nominationId, Guid musicalInstrumentId);
    Task<List<Nomination>> GetAllAsync(CancellationToken ct);
    Task<Nomination?> GetByIdWithInstrumentsAsync(Guid id, CancellationToken ct);
}
