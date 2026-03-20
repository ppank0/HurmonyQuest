using ContestService.DAL.Context;
using ContestService.DAL.Entities;
using ContestService.DAL.Models;
using ContestService.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContestService.DAL.Repositories.Implementations;

internal class MusicalInstrumentRepository(AppDbContext context) : RepositoryBase<MusicalInstrument>(context), IMusicalInstrumentRepository
{
    public async Task<List<MusicalInstrumentExtendedModel>> GetAllWithNominationsAsync(CancellationToken ct)
    {
        return await _context.MusicalInstruments.Select(x => new MusicalInstrumentExtendedModel
        {
            Id = x.Id,
            Name = x.Name,
            NominationId = x.NominationId,
            NominationName = x.Nomination.Name
        }).ToListAsync(ct);
    }
}
