using ContestService.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestService.BLL.Interfaces;
public interface INominationService
{
    Task<NominationModel> GetNominationByIdAsync(Guid id, CancellationToken ct);
    Task<List<NominationModel>> GetAllAsync(CancellationToken ct);
    Task<NominationModel> DeleteNominationAsync(Guid id, CancellationToken ct);
    Task<NominationModel> CreateNominationAsync(NominationModel nomination, CancellationToken ct);
    Task<NominationModel> UpdateNominationAsync(NominationModel nomination, CancellationToken ct);
}
