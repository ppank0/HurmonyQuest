using ContestService.DAL.Context;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContestService.DAL.Repositories.Implementations;

public class MusicalInstrumentRepository(AppDbContext context) : RepositoryBase<MusicalInstrument>(context), IMusicalInstrumentRepository
{
    public async Task<MusicalInstrument?> GetByIdWithNominationAsync(Guid id, CancellationToken ct)
    {
        return await _context.MusicalInstruments.Where(x => x.Id == id).Include(x => x.Nomination).FirstOrDefaultAsync(ct);
    }
}
