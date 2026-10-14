using ContestService.DAL.Context;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContestService.DAL.Repositories.Implementations;

public class NominationRepository(AppDbContext context) : RepositoryBase<Nomination>(context), INominationRepository
{
    public bool IsMusicalInstrumentInNomination(Guid nominationId, Guid musicalInstrumentId)
    {
        return _context.Nominations.Any(n => n.Id == nominationId &&
                            n.MusicalInstruments!.Any(i => i.Id == musicalInstrumentId));
    }

    public async Task<List<Nomination>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Nominations.Include(n => n.MusicalInstruments).ToListAsync(ct);
    }
    public async Task<Nomination?> GetByIdWithInstrumentsAsync(Guid id, CancellationToken ct)
    {
        return await _context.Nominations.Where(x => x.Id == id)
            .Include(n => n.MusicalInstruments).FirstOrDefaultAsync(ct);
    }

}
