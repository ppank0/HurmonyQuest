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
public class MusicalInstrumentService (IRepositoryBase<MusicalInstrument> _repositoryBase) : IMusicalInstrumentService
{
    public async Task<MusicalInstrumentModel> CreateAsync(MusicalInstrumentModel musicalInstrument, CancellationToken ct)
    {
        MusicalInstrument createdinstrument = await _repositoryBase.CreateAsync(new MusicalInstrument
        {
            Name = musicalInstrument.Name,
            NominationId = musicalInstrument.Nomination.Id,
            
        }, ct);

        return new MusicalInstrumentModel
        {
            Id = createdinstrument.Id,
            Name = createdinstrument.Name,
            NominationId = createdinstrument.Nomination.Id,
        };
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var musicalInstrument = _repositoryBase.FindByCondition(p => p.Id == id, ct).FirstOrDefault();

        if (musicalInstrument is not null)
        {
            await _repositoryBase.DeleteAsync(musicalInstrument, ct);
        }
        else
        {
            throw new NotFoundException($"Instrument with id {id} was not found");
        }
    }

    public async Task<List<MusicalInstrumentModel>> GetAllAsync(CancellationToken ct)
    {
        var entitiesList = await _repositoryBase.GetAllToListAsync(ct);
        var MusicalInstrumentModelList = entitiesList.Select(x => new MusicalInstrumentModel
        {
            Id = x.Id,
            Name = x.Name,
            NominationId = x.Nomination.Id,
        }).ToList();

        return MusicalInstrumentModelList;
    }

    public async Task<MusicalInstrumentModel> GetAsync(Guid id, CancellationToken ct)
    {
        var instrumentList = await _repositoryBase.FindByConditionAsync(s => s.Id == id, ct);
        var musicalInstrument = instrumentList.FirstOrDefault();

        if (musicalInstrument is null)
        {
            throw new NotFoundException($"Instrument with id {id} was not found");
        }

        return new MusicalInstrumentModel 
        { Id = musicalInstrument.Id, Name = musicalInstrument.Name, NominationId = musicalInstrument.NominationId };
    }

    public async Task<MusicalInstrumentModel> UpdateAsync(MusicalInstrumentModel model, CancellationToken ct)
    {
        var instrumentList = await _repositoryBase.FindByConditionAsync(s => s.Id == model.Id, ct);
        var foundInstrument = instrumentList.FirstOrDefault();

        if (foundInstrument is not null)
        {
            foundInstrument.Name = model.Name;
            foundInstrument.NominationId = model.NominationId;
        }

        MusicalInstrument updated = await _repositoryBase.UpdateAsync(foundInstrument, ct);
        return new MusicalInstrumentModel { Id = updated.Id, Name = updated.Name, NominationId = updated.NominationId};
    }
}
