using AutoMapper;
using ContestService.BLL.Exceptions;
using ContestService.BLL.Interfaces;
using ContestService.BLL.Models;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContestService.BLL.Services;
public class MusicalInstrumentService(IMapper mapper,
    IRepositoryBase<Nomination> nominationRepo, IMusicalInstrumentRepository instrumentRepo) : IMusicalInstrumentService
{
    public async Task<MusicalInstrumentModel> CreateAsync(MusicalInstrumentModel musicalInstrument, CancellationToken ct)
    {
        var instrument = mapper.Map<MusicalInstrument>(musicalInstrument);
        var createdInstrument = await instrumentRepo.CreateAsync(instrument, ct);

        return mapper.Map<MusicalInstrumentModel>(createdInstrument);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var musicalInstruments = await instrumentRepo.FindByConditionAsync(p => p.Id == id, ct);
        var musicalInstrument = musicalInstruments.FirstOrDefault();

        if (musicalInstrument is not null)
        {
            await instrumentRepo.DeleteAsync(musicalInstrument, ct);
        }
        else
        {
            throw new NotFoundException($"Instrument with id {id} was not found");
        }
    }

    public async Task<List<MusicalInstrumentModel>> GetAllAsync(CancellationToken ct)
    {
        var entitiesList = await instrumentRepo.GetAllWithNominationsAsync(ct);

        return mapper.Map<List<MusicalInstrumentModel>>(entitiesList);
    }

    public async Task<MusicalInstrumentModel> GetAsync(Guid id, CancellationToken ct)
    {
        var instrumentList = await instrumentRepo.FindByConditionAsync(s => s.Id == id, ct);
        var musicalInstrument = instrumentList.FirstOrDefault();

        if (musicalInstrument is null)
        {
            throw new NotFoundException($"Instrument with id {id} was not found");
        }

        var model = mapper.Map<MusicalInstrumentModel>(musicalInstrument);
        var nominationName = await nominationRepo.FindByCondition(x => x.Id == model.NominationId, ct).Select(x => x.Name).FirstOrDefaultAsync(ct);
        model.NominationName = nominationName ?? null;
        return model;
    }

    public async Task<MusicalInstrumentModel> UpdateAsync(MusicalInstrumentModel model, CancellationToken ct)
    {
        var instrumentList = await instrumentRepo.FindByConditionAsync(s => s.Id == model.Id, ct);
        var entityToUpdate = instrumentList.FirstOrDefault();

        if (entityToUpdate is null)
        {
            throw new NotFoundException($"Instrument with id {model.Id} was not found");
        }

        mapper.Map(model, entityToUpdate);
        await instrumentRepo.UpdateAsync(entityToUpdate, ct);

        return mapper.Map<MusicalInstrumentModel>(entityToUpdate);
    }
}
