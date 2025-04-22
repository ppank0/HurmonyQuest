using ContestService.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestService.BLL.Interfaces;
public interface INominationService
{
    Task<NominationModel> GetAsync(Guid id, CancellationToken ct);
    Task<List<NominationModel>> GetAllAsync(CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
    Task<NominationModel> CreateAsync(NominationModel nomination, CancellationToken ct);
    Task<NominationModel> UpdateAsync(NominationModel nomination, CancellationToken ct);
}
