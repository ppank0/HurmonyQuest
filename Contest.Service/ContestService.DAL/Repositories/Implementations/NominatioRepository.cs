using ContestService.DAL.Context;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestService.DAL.Repositories.Implementations;
public class NominationRepository(AppDbContext context) : RepositoryBase<Nomination>(context), INominationRepository
{
    protected readonly AppDbContext _context = context;

    public bool IsMusicalInstrumentInNomination(Guid NominationId, Guid MusicalInstrumentId)
    {
        return _context.Nominations
        .Any(n => n.Id == NominationId && n.Instruments.Any(i => i.Id == MusicalInstrumentId));

    }

    public Guid GetNominationIdByInstrumentId(Guid instrumentId)
    {
        return _context.Nominations.Where(x=>x.Instruments.Any(i=>i.Id==instrumentId)).Select(n => n.Id).FirstOrDefault();
    }
}
