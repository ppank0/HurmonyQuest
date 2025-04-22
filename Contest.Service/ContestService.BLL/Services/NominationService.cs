using AutoMapper;
using ContestService.BLL.Exceptions;
using ContestService.BLL.Interfaces;
using ContestService.BLL.Models;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Interfaces;

namespace ContestService.BLL.Services;
public class NominationService(IRepositoryBase<Nomination> repository, IMapper mapper) : INominationService
{
    public async Task<NominationModel> CreateAsync(NominationModel nomination, CancellationToken ct)
    {
        var newNomination = mapper.Map<Nomination>(nomination);
        var createdNomination = await repository.CreateAsync(newNomination, ct);

        return mapper.Map<NominationModel>(createdNomination);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var nomination = repository.FindByCondition(p => p.Id == id, ct).FirstOrDefault();

        if (nomination is not null)
        {
            await repository.DeleteAsync(nomination, ct);
        }
        else
        {
            throw new NotFoundException($"Nomination with id {id} was not found");
        }
    }

    public async Task<List<NominationModel>> GetAllAsync(CancellationToken ct)
    {
        var entitiesList = await repository.GetAllToListAsync(ct);

        return mapper.Map<List<NominationModel>>(entitiesList);
    }

    public async Task<NominationModel> GetAsync(Guid id, CancellationToken ct)
    {
        var nominationList = await repository.FindByConditionAsync(s => s.Id == id, ct);
        var nomination = nominationList.FirstOrDefault();

        if (nomination is null)
        {
            throw new NotFoundException($"Nomination with id {id} was not found");
        }

        return new NominationModel { Id = nomination.Id, Name = nomination.Name };
    }

    public async Task<NominationModel> UpdateAsync(NominationModel nomination, CancellationToken ct)
    {
        var nominationList = await repository.FindByConditionAsync(s => s.Id == nomination.Id, ct);
        var entityToUpdate = nominationList.FirstOrDefault();

        if (entityToUpdate is null)
        {
            throw new NotFoundException($"Nomination with id {nomination.Id} was not found");
        }

        mapper.Map(nomination, entityToUpdate);
        var updated = await repository.UpdateAsync(entityToUpdate, ct);

        return mapper.Map<NominationModel>(updated);
    }
}
