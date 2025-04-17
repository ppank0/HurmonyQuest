using ContestService.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestService.BLL.Interfaces;
public interface IStageService
{
    Task<StageModel> GetStageById(Guid id, CancellationToken ct);
    Task<List<StageModel>> GetAllAsync(CancellationToken ct);
    Task<StageModel> DeleteStageAsync(Guid id, CancellationToken ct);
    Task<StageModel> CreateStageAsync(StageModel stage, CancellationToken ct);
    Task<StageModel> UpdateStageAsync(StageModel stage, CancellationToken ct);
}
