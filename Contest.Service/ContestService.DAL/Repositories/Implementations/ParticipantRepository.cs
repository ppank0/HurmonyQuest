using ContestService.DAL.Context;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestService.DAL.Repositories.Implementations;
public class ParticipantRepository(AppDbContext context) : RepositoryBase<Participant>(context), IParticipantRepository
{
    public async Task<List<Participant>> GetAllWithRelationsAsync(CancellationToken cancellationToken)
    {
        return await context.Participants
            .Include(p => p.MusicalInstrument)
            .ToListAsync(cancellationToken);
    }
}
