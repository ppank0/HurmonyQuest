using AutoMapper;
using ContestService.BLL.Exceptions;
using ContestService.BLL.Interfaces;
using ContestService.BLL.Models;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Nomination nomination = nominationList.FirstOrDefault();

        if( nomination is null)
        {
            throw new NotFoundException($"Nomination with id {id} was not found");
        }

        return new NominationModel {Id = nomination.Id,  Name = nomination.Name };
    }

    public async Task<NominationModel> UpdateAsync(NominationModel nomination, CancellationToken ct)
    {
        var nominationList = await repository.FindByConditionAsync(s => s.Id == nomination.Id, ct);
        var foundNomination = nominationList.FirstOrDefault();

        if (foundNomination is null)
        {
            throw new NotFoundException($"Nomination with id {nomination.Id} was not found");
        }

        foundNomination.Name = nomination.Name;
        Nomination updated = await repository.UpdateAsync(foundNomination, ct);

        return new NominationModel { Id = updated.Id, Name = updated.Name};
    }
}
