using ContestService.DAL.Entities;

namespace ContestService.DAL.Repositories.Interfaces;
public interface INominationRepository : IRepositoryBase<Nomination>
{
    public bool IsMusicalInstrumentInNomination(Guid NominationId, Guid MusicalInstrumentId);
    
}
