using ContestService.BLL.Exceptions;
using ContestService.BLL.Interfaces;
using ContestService.BLL.Models;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Interfaces;

namespace ContestService.BLL.Services;
public class StageService(IRepositoryBase<Stage> _repositoryBase) : IStageService
{
    public async Task<StageModel> CreateAsync(StageModel stage, CancellationToken ct)
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

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var stage = _repositoryBase.FindByCondition(p => p.Id == id, ct).FirstOrDefault();
        if (stage is  null)
        {
            throw new NotFoundException($"Stage with id {id} was not found");
        }
        await _repositoryBase.DeleteAsync(stage, ct);
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

    public async Task<StageModel> GetAsync(Guid id, CancellationToken ct)
    {
        var stageList = await _repositoryBase.FindByConditionAsync(s => s.Id == id, ct);
        var stage = stageList.FirstOrDefault();
        if (stage is null)
        {
            throw new NotFoundException($"Stage with id {id} was not found");
        }

        return new StageModel 
        { 
            Id = stage.Id, 
            Name = stage.Name, 
            StartDate = stage.StartDate, 
            EndDate = stage.EndDate 
        };
    }

    public async Task<StageModel> UpdateAsync(StageModel stage, CancellationToken ct)
    {
        var stageList = await _repositoryBase.FindByConditionAsync(s => s.Id == stage.Id, ct);
        var foundStage = stageList.FirstOrDefault();
        if (foundStage is not null)
        {
            foundStage.Name = stage.Name;
            foundStage.StartDate = stage.StartDate;
            foundStage.EndDate = stage.EndDate;
        }
        Stage updated = await _repositoryBase.UpdateAsync(foundStage, ct);
        return new StageModel { Id = updated.Id, Name = updated.Name, StartDate = updated.StartDate, EndDate = updated.EndDate };
    }
}
