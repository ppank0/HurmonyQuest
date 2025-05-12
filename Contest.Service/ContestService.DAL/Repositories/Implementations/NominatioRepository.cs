using ContestService.DAL.Context;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace ContestService.DAL.Repositories.Implementations;
public class NominationRepository(AppDbContext context, IDistributedCache cache) : RepositoryBase<Nomination>(context, cache), INominationRepository
{
    public bool IsMusicalInstrumentInNomination(Guid NominationId, Guid MusicalInstrumentId)
    {
        return _context.Nominations
        .Any(n => n.Id == NominationId && n.Instruments.Any(i => i.Id == MusicalInstrumentId));

    }

}
