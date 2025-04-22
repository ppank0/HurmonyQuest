using ContestService.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestService.BLL.Interfaces;
public interface IMusicalInstrumentService
{
    Task<List<MusicalInstrumentModel>> GetAllAsync(CancellationToken ct);
    Task<MusicalInstrumentModel> GetAsync(Guid id, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
    Task<MusicalInstrumentModel> CreateAsync(MusicalInstrumentModel musicalInstrument, CancellationToken ct);
    Task<MusicalInstrumentModel> UpdateAsync(MusicalInstrumentModel model, CancellationToken ct);

}
