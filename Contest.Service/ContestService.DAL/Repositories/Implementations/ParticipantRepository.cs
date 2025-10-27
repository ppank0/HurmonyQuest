using ContestService.DAL.Context;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContestService.DAL.Repositories.Implementations;

public class ParticipantRepository : RepositoryBase<Participant>, IParticipantRepository
{
    private readonly AppDbContext _context;
    public ParticipantRepository(AppDbContext context): base(context)
    {
        _context = context;
    }
    public async Task<List<Participant>> GetAllWithRelationsAsync(CancellationToken cancellationToken)
    {
        return await _context.Participants
            .Include(p => p.MusicalInstrument)
            .ToListAsync(cancellationToken);
    }
}
