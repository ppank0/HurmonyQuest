using ContestService.DAL.Entities;

namespace ContestService.DAL.Repositories.Interfaces;

public interface IMusicalInstrumentRepository : IRepositoryBase<MusicalInstrument>
{
    Task<MusicalInstrument?> GetByIdWithNominationAsync(Guid id, CancellationToken ct);
}
