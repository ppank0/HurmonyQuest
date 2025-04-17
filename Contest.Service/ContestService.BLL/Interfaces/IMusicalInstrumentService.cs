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
    Task<MusicalInstrumentModel> GetMusicalInstrumentByIdAsync(Guid id, CancellationToken ct);
    Task<MusicalInstrumentModel> DeleteMusicalInstrumentAsync(Guid id, CancellationToken ct);
    Task<MusicalInstrumentModel> CreateMusicalInstrumentAsync(MusicalInstrumentModel musicalInstrument, CancellationToken ct);
    Task<MusicalInstrumentModel> UpdateMusicalInstrumentAsync(MusicalInstrumentModel model, CancellationToken ct);

}
