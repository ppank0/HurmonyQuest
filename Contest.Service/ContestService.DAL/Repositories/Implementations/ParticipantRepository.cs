using ContestService.DAL.Context;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContestService.DAL.Repositories.Implementations;

public class ParticipantRepository(AppDbContext context)  : RepositoryBase<Participant>(context), IParticipantRepository
{
    public async Task<List<Participant>> GetAllWithRelationsAsync(CancellationToken cancellationToken)
    {
        return await _context.Participants
            .Include(p => p.MusicalInstrument)
            .ToListAsync(cancellationToken);
    }
}
