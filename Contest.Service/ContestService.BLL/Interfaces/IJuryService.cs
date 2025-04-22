using ContestService.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestService.BLL.Interfaces;
public interface IJuryService
{
    Task<JuryModel> GetAsync(Guid id, CancellationToken ct);
    Task<List<JuryModel>> GetAllAsync(CancellationToken ct);
    Task<JuryModel> CreateAsync(JuryModel juryModel, CancellationToken ct);
    Task<JuryModel> UpdateAsync(JuryModel model, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
