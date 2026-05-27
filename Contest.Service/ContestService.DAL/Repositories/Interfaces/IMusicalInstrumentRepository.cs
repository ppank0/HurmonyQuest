using ContestService.DAL.Entities;
using ContestService.DAL.Models;

namespace ContestService.DAL.Repositories.Interfaces;

public interface IMusicalInstrumentRepository : IRepositoryBase<MusicalInstrument>
{
    Task<List<MusicalInstrumentExtendedModel>> GetAllWithNominationsAsync(CancellationToken ct);
}
