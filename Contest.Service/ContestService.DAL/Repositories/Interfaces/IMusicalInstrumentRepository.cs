using ContestService.DAL.Entities;
using ContestService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestService.DAL.Repositories.Interfaces;

public interface IMusicalInstrumentRepository
{
    Task<List<MusicalInstrumentExtendedModel>> GetAllWithNominationsAsync(CancellationToken ct);
}
