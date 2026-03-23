using ContestService.DAL.Context;
using ContestService.DAL.Entities;
using ContestService.DAL.Models;

using ContestService.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContestService.DAL.Repositories.Implementations;

public class ParticipantRepository(AppDbContext context) : RepositoryBase<Participant>(context), IParticipantRepository
{
    public async Task<List<ParticipantExtendedModel>> GetAllWithDetails(CancellationToken ct)
    {
        return await context.Participants.Select(p => new ParticipantExtendedModel
        {
            Id = p.Id,
            Name = p.Name,
            Surname = p.Surname,
            Birthday = p.Birthday,
            MusicalInstrumentId = p.MusicalInstrumentId,
            MusicalInstrumentName = p.MusicalInstrument.Name,
            NominationId = p.MusicalInstrument.NominationId,
            NominationName = p.MusicalInstrument.Nomination.Name
        }).ToListAsync(ct);
    }
}
