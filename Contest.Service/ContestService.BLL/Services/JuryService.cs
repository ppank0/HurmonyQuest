using AutoMapper;
using ContestService.BLL.Exceptions;
using ContestService.BLL.Interfaces;
using ContestService.BLL.Models;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Interfaces;

namespace ContestService.BLL.Services;
public class JuryService(IRepositoryBase<Jury> repository, IMapper mapper) : IJuryService
{
    public async Task<JuryModel> CreateAsync(JuryModel juryModel, CancellationToken ct)
    {
        var newJury = mapper.Map<Jury>(juryModel);

        var createdEntity = await repository.CreateAsync(newJury, ct);

        return mapper.Map<JuryModel>(createdEntity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var juries = await repository.FindByConditionAsync(j => j.Id == id, ct);
        var jury = juries.FirstOrDefault();

        if (jury is null)
        {
            throw new NotFoundException($"Jury with id {id} was not found");
        }

        await repository.DeleteAsync(jury, ct);
    }

    public async Task<List<JuryModel>> GetAllAsync(CancellationToken ct)
    {
        var entities = await repository.GetAllToListAsync(ct);

        return mapper.Map<List<JuryModel>>(entities);
    }

    public async Task<JuryModel> GetAsync(Guid id, CancellationToken ct)
    {
        var juryList = await repository.FindByConditionAsync(j => j.Id == id, ct);
        Jury? jury = juryList.FirstOrDefault();

        if (jury is null)
        {
            throw new NotFoundException("entity jury was not found");
        }
        return mapper.Map<JuryModel>(jury);
    }

    public async Task<JuryModel> UpdateAsync(JuryModel model, CancellationToken ct)
    {
        var juryList = await repository.FindByConditionAsync(j => j.Id == model.Id, ct);
        Jury? entityToUpdate = juryList.FirstOrDefault();

        if(entityToUpdate is null)
        {
            throw new NotFoundException($"Jury with with {model.Id}");
        }

        mapper.Map(model, entityToUpdate);

        var updatedJury = await repository.UpdateAsync(entityToUpdate, ct);

        return mapper.Map<JuryModel>(updatedJury);
    }
}
