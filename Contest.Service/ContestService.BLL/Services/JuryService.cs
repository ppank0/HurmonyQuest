using ContestService.BLL.Exceptions;
using ContestService.BLL.Interfaces;
using ContestService.BLL.Models;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Implementations;
using ContestService.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestService.BLL.Services;
public class JuryService (IRepositoryBase <Jury> repositoryBase) : IJuryService
{
    private readonly IRepositoryBase<Jury> _repositoryBase;

    public async Task<JuryModel> CreateJuryAsync(JuryModel juryModel, CancellationToken ct)
    {
        Jury newJury = new Jury
        {
            Name =juryModel.Name,
            Surname = juryModel.Surname,
            Birthday = juryModel.Birthday
        };

        var createdEntity = await _repositoryBase.CreateAsync(newJury, ct);

        return new JuryModel
        {
            Id = createdEntity.Id,
            Name = createdEntity.Name,
            Surname = createdEntity.Surname,
            Birthday = createdEntity.Birthday
        };
    }

    public async Task<JuryModel> DeleteJuryAsync(Guid id, CancellationToken ct)
    {
        var query = _repositoryBase.FindByCondition(j => j.Id == id, ct).FirstOrDefault();
        if (query != null)
        {
            await _repositoryBase.DeleteAsync(query, ct);
        }
        {
            throw new NotFoundException($"Jury with id {id} was not found");
        }
    }

    public async Task<List<JuryModel>> GetAllAsync(CancellationToken ct)
    {
        var entities = await _repositoryBase.GetAllToListAsync(ct);
        var model = entities.Select(e => new JuryModel
        {
            Id = e.Id,
            Name = e.Name,
            Surname = e.Surname,
            Birthday = e.Birthday,
        }).ToList();

        return model;
    }

    public async Task<JuryModel> GetJuryAsync(Guid id, CancellationToken ct)
    {
        var resultList = await _repositoryBase.FindByConditionToListAsync(j => j.Id == id, ct);
        Jury result = resultList.FirstOrDefault();
        return result == null
            ? throw new NotFoundException("entity jury was not found")
            : new JuryModel()
            {
                Name = result.Name,
                Surname = result.Surname,
                Birthday = result.Birthday
            };
    }

    public async Task<JuryModel> UpdateJuryAsync(JuryModel model, CancellationToken ct)
    {
        var query = await _repositoryBase.FindByConditionToListAsync(j => j.Id == model.Id, ct);
        Jury result = query.FirstOrDefault();

        result.Name = model.Name;
        result.Surname = model.Surname;
        result.Birthday = model.Birthday;

        var updatedJury = await _repositoryBase.UpdateAsync(result, ct);

        return new JuryModel
        {
            Name = updatedJury.Name,
            Surname = updatedJury.Surname,
            Birthday = updatedJury.Birthday
        };
    }
}
