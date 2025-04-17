using ContestService.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestService.BLL.Interfaces;
internal interface IJuryService
{
    Task<JuryModel> GetJuryAsync(Guid id, CancellationToken ct);
    Task<List<JuryModel>> GetAllAsync(CancellationToken ct);
    Task<JuryModel> CreateJuryAsync(JuryModel juryModel, CancellationToken ct);
    Task<JuryModel> UpdateJuryAsync(JuryModel model, CancellationToken ct);
    Task<JuryModel> DeleteJuryAsync(Guid id, CancellationToken ct);
}
