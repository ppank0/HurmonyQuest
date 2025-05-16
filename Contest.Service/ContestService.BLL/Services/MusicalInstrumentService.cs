using AutoMapper;
using ContestService.BLL.Exceptions;
using ContestService.BLL.Interfaces;
using ContestService.BLL.Models;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Interfaces;

namespace ContestService.BLL.Services;
public class MusicalInstrumentService(IRepositoryBase<MusicalInstrument> repository, IMapper mapper) : IMusicalInstrumentService
{
    public async Task<MusicalInstrumentModel> CreateAsync(MusicalInstrumentModel musicalInstrument, CancellationToken ct)
    {
        var instrument = mapper.Map<MusicalInstrument>(musicalInstrument);
        var createdInstrument = await repository.CreateAsync(instrument, ct);

        return mapper.Map<MusicalInstrumentModel>(createdInstrument);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var musicalInstruments = await repository.FindByConditionAsync(p => p.Id == id, ct);
        var musicalInstrument = musicalInstruments.FirstOrDefault();

        if (musicalInstrument is not null)
        {
            await repository.DeleteAsync(musicalInstrument, ct);
        }
        else
        {
            throw new NotFoundException($"Instrument with id {id} was not found");
        }
    }

    public async Task<List<MusicalInstrumentModel>> GetAllAsync(CancellationToken ct)
    {
        var entitiesList = await repository.GetAllToListAsync(ct);

        return mapper.Map<List<MusicalInstrumentModel>>(entitiesList);
    }

    public async Task<MusicalInstrumentModel> GetAsync(Guid id, CancellationToken ct)
    {
        var instrumentList = await repository.FindByConditionAsync(s => s.Id == id, ct);
        var musicalInstrument = instrumentList.FirstOrDefault();

        if (musicalInstrument is null)
        {
            throw new NotFoundException($"Instrument with id {id} was not found");
        }

        return mapper.Map<MusicalInstrumentModel>(musicalInstrument);
    }

    public async Task<MusicalInstrumentModel> UpdateAsync(MusicalInstrumentModel model, CancellationToken ct)
    {
        var instrumentList = await repository.FindByConditionAsync(s => s.Id == model.Id, ct);
        var entityToUpdate = instrumentList.FirstOrDefault();

        if (entityToUpdate is null)
        {
            throw new NotFoundException($"Instrument with id {model.Id} was not found");
        }

        mapper.Map(model, entityToUpdate);
        await repository.UpdateAsync(entityToUpdate, ct);

        return mapper.Map<MusicalInstrumentModel>(entityToUpdate);
    }
}
