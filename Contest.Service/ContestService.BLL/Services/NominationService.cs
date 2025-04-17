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
public class NominationService(IRepositoryBase<Nomination> repositoryBase) : INominationService
{
    private readonly IRepositoryBase<Nomination> _repositoryBase;

    public async Task<NominationModel> CreateNominationAsync(NominationModel nomination, CancellationToken ct)
    {
        var newNomination = new Nomination { Name = nomination.Name };
        var createdNomination = await _repositoryBase.CreateAsync(newNomination, ct);
        return new NominationModel { Id = createdNomination.Id, Name = createdNomination.Name };
    }

    public async Task<NominationModel> DeleteNominationAsync(Guid id, CancellationToken ct)
    {
        var query = _repositoryBase.FindByCondition(p => p.Id == id, ct).FirstOrDefault();
        if (query != null)
        {
            await _repositoryBase.DeleteAsync(query, ct);
        }
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

    public async Task<NominationModel> GetNominationByIdAsync(Guid id, CancellationToken ct)
    {
        var nominationList = await _repositoryBase.FindByConditionToListAsync(s => s.Id == id, ct);
        Nomination nomination = nominationList.FirstOrDefault();
        return nomination == null
            ? throw new NotFoundException($"Nomination with id {id} was not found")
            : new NominationModel {Id = nomination.Id,  Name = nomination.Name };
    }

    public async Task<NominationModel> UpdateNominationAsync(NominationModel nomination, CancellationToken ct)
    {
        var nominationList = await _repositoryBase.FindByConditionToListAsync(s => s.Id == nomination.Id, ct);
        var foundNomination = nominationList.FirstOrDefault();
        if (foundNomination != null)
        {
            foundNomination.Name = nomination.Name;
        }
        Nomination updated = await _repositoryBase.UpdateAsync(foundNomination, ct);
        return new NominationModel { Id = updated.Id, Name = updated.Name};
    }
}
