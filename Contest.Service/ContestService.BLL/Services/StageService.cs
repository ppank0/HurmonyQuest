using ContestService.BLL.Exceptions;
using ContestService.BLL.Interfaces;
using ContestService.BLL.Models;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Interfaces;

namespace ContestService.BLL.Services;
public class StageService(IRepositoryBase<Stage> repositoryBase) : IStageService
{
    private readonly IRepositoryBase<Stage> _repositoryBase = repositoryBase;
    public async Task<StageModel> CreateStageAsync(StageModel stage, CancellationToken ct)
    {
        Stage createdStage = await _repositoryBase.CreateAsync(new Stage
        {
            Name = stage.Name,
            StartDate = stage.StartDate,
            EndDate = stage.EndDate
        }, ct);
        return new StageModel
        {
            Id = createdStage.Id,
            Name = createdStage.Name,
            StartDate = createdStage.StartDate,
            EndDate = createdStage.EndDate,
        };
    }

    public async Task<StageModel> DeleteStageAsync(Guid id, CancellationToken ct)
    {
        var query = _repositoryBase.FindByCondition(p => p.Id == id, ct).FirstOrDefault();
        if (query != null)
        {
            await _repositoryBase.DeleteAsync(query, ct);
        }
        {
            throw new NotFoundException($"Stage with id {id} was not found");
        }
    }

    public async Task<List<StageModel>> GetAllAsync(CancellationToken ct)
    {
        var entitiesList = await _repositoryBase.GetAllToListAsync(ct);
        var stageModelList = entitiesList.Select(x => new StageModel
        {
            Id = x.Id,
            Name = x.Name,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
        }).ToList();

        return stageModelList;
    }

    public async Task<StageModel> GetStageById(Guid id, CancellationToken ct)
    {
        var stageList = await _repositoryBase.FindByConditionToListAsync(s => s.Id == id, ct);
        Stage stage = stageList.FirstOrDefault();
        return stage == null
            ? throw new NotFoundException($"Stage with id {id} was not found")
            : new StageModel {Id = stage.Id, Name = stage.Name, StartDate = stage.StartDate, EndDate = stage.EndDate };
    }

    public async Task<StageModel> UpdateStageAsync(StageModel stage, CancellationToken ct)
    {
        var stageList = await _repositoryBase.FindByConditionToListAsync(s => s.Id == stage.Id, ct);
        var foundStage = stageList.FirstOrDefault();
        if (foundStage != null)
        {
            foundStage.Name = stage.Name;
            foundStage.StartDate = stage.StartDate;
            foundStage.EndDate = stage.EndDate;
        }
        Stage updated = await _repositoryBase.UpdateAsync(foundStage, ct);
        return new StageModel { Id = updated.Id, Name = updated.Name, StartDate = updated.StartDate, EndDate = updated.EndDate };
    }
}
