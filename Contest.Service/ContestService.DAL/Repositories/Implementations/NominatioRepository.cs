using ContestService.DAL.Context;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Interfaces;

namespace ContestService.DAL.Repositories.Implementations;
public class NominationRepository(AppDbContext context) : RepositoryBase<Nomination>(context), INominationRepository
{
    public bool IsMusicalInstrumentInNomination(Guid NominationId, Guid MusicalInstrumentId)
    {
        var nomination = _context.Nominations
            .FirstOrDefault(n => n.Id == NominationId);

        return nomination != null &&
               nomination.Instruments != null &&
               nomination.Instruments.Any(i => i.Id == MusicalInstrumentId);
    }

}
