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
public class NominationService(IRepositoryBase<Nomination> _repositoryBase) : INominationService
{
    public async Task<NominationModel> CreateAsync(NominationModel nomination, CancellationToken ct)
    {
        var newNomination = new Nomination { Name = nomination.Name };
        var createdNomination = await _repositoryBase.CreateAsync(newNomination, ct);
        return new NominationModel { Id = createdNomination.Id, Name = createdNomination.Name };
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var nomination = _repositoryBase.FindByCondition(p => p.Id == id, ct).FirstOrDefault();
        if (nomination is not null)
        {
            await _repositoryBase.DeleteAsync(nomination, ct);
        }
        else
        {
            throw new NotFoundException($"Nomination with id {id} was not found");
        }
    }

    public async Task<List<NominationModel>> GetAllAsync(CancellationToken ct)
    {
        var entitiesList = await _repositoryBase.GetAllToListAsync(ct);
        var nominationModelList = entitiesList.Select(x => new NominationModel
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();

        return nominationModelList;
    }

    public async Task<NominationModel> GetAsync(Guid id, CancellationToken ct)
    {
        var nominationList = await _repositoryBase.FindByConditionAsync(s => s.Id == id, ct);
        Nomination nomination = nominationList.FirstOrDefault();
        if( nomination is null)
        {
            throw new NotFoundException($"Nomination with id {id} was not found");
        }

        return new NominationModel {Id = nomination.Id,  Name = nomination.Name };
    }

    public async Task<NominationModel> UpdateAsync(NominationModel nomination, CancellationToken ct)
    {
        var nominationList = await _repositoryBase.FindByConditionAsync(s => s.Id == nomination.Id, ct);
        var foundNomination = nominationList.FirstOrDefault();
        if (foundNomination is null)
        {
            throw new NotFoundException($"Nomination with id {nomination.Id} was not found");
        }
        foundNomination.Name = nomination.Name;
        Nomination updated = await _repositoryBase.UpdateAsync(foundNomination, ct);
        return new NominationModel { Id = updated.Id, Name = updated.Name};
    }
}
