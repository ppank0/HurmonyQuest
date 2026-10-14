using ContestService.DAL.Context;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContestService.DAL.Repositories.Implementations;

public class ParticipantRepository(AppDbContext context) : RepositoryBase<Participant>(context), IParticipantRepository
{
    public async Task<List<Participant>> GetAllWithDetails(CancellationToken ct)
    {
        return await context.Participants.Include(x => x.MusicalInstrument!.Nomination).Select(p => new Participant
        {
            Id = p.Id,
            Name = p.Name,
            Surname = p.Surname,
            Birthday = p.Birthday,
            MusicalInstrumentId = p.MusicalInstrumentId,
            MusicalInstrument = p.MusicalInstrument,
        }).AsNoTracking().ToListAsync(ct);
    }

    public async Task<Participant?> GetByIdWithDetails(Guid id, CancellationToken ct)
    {
        return await context.Participants.Where(x => x.Id == id)
            .Include(i => i.MusicalInstrument!.Nomination).FirstOrDefaultAsync(ct);
    }
}
