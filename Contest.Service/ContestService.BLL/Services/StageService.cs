using AutoMapper;
using ContestService.BLL.Exceptions;
using ContestService.BLL.Interfaces;
using ContestService.BLL.Models;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Interfaces;

namespace ContestService.BLL.Services;
public class StageService(IRepositoryBase<Stage> repository, IMapper mapper) : IStageService
{
    public async Task<StageModel> CreateAsync(StageModel stage, CancellationToken ct)
    {
        var newStage = mapper.Map<Stage>(stage);
        var createdStage = await repository.CreateAsync(newStage, ct);

        return mapper.Map<StageModel>(createdStage);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var stage = repository.FindByCondition(p => p.Id == id, ct).FirstOrDefault();

        if (stage is  null)
        {
            throw new NotFoundException($"Stage with id {id} was not found");
        }

        await repository.DeleteAsync(stage, ct);
    }

    public async Task<List<StageModel>> GetAllAsync(CancellationToken ct)
    {
        var entitiesList = await repository.GetAllToListAsync(ct);

        return mapper.Map<List<StageModel>>(entitiesList);
    }

    public async Task<StageModel> GetAsync(Guid id, CancellationToken ct)
    {
        var stageList = await repository.FindByConditionAsync(s => s.Id == id, ct);
        var stage = stageList.FirstOrDefault();

        if (stage is null)
        {
            throw new NotFoundException($"Stage with id {id} was not found");
        }

        return mapper.Map<StageModel>(stage);
    }

    public async Task<StageModel> UpdateAsync(StageModel stage, CancellationToken ct)
    {
        var stageList = await repository.FindByConditionAsync(s => s.Id == stage.Id, ct);
        var entityToUpdate = stageList.FirstOrDefault();

        if (entityToUpdate is null)
        { 
            throw new NotFoundException($"Stage with id {stage.Id} was not found");
        }
        mapper.Map(stage, entityToUpdate);
        Stage updated = await repository.UpdateAsync(entityToUpdate, ct);

        return mapper.Map<StageModel>(updated);
    }
}
