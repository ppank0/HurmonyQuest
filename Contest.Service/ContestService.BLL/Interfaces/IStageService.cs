using ContestService.BLL.Models;

namespace ContestService.BLL.Interfaces;
public interface IStageService
{
    Task<StageModel> GetAsync(Guid id, CancellationToken ct);
    Task<List<StageModel>> GetAllAsync(CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
    Task<StageModel> CreateAsync(StageModel stage, CancellationToken ct);
    Task<StageModel> UpdateAsync(StageModel stage, CancellationToken ct);
}
