using ContestService.BLL.Exceptions;
using ContestService.BLL.Interfaces;
using ContestService.BLL.Models;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Interfaces;

namespace ContestService.BLL.Services;
public class JuryService(IRepositoryBase<Jury> _repositoryBase) : IJuryService
{
    public async Task<JuryModel> CreateAsync(JuryModel juryModel, CancellationToken ct)
    {
        Jury newJury = new Jury
        {
            Name = juryModel.Name,
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

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var jury = _repositoryBase.FindByCondition(j => j.Id == id, ct).FirstOrDefault();

        if (jury is null)
        {
            throw new NotFoundException($"Jury with id {id} was not found");
        }
        await _repositoryBase.DeleteAsync(jury, ct);
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

    public async Task<JuryModel> GetAsync(Guid id, CancellationToken ct)
    {
        var juryList = await _repositoryBase.FindByConditionAsync(j => j.Id == id, ct);
        Jury jury = juryList.FirstOrDefault();

        if (jury is null)
        {
            throw new NotFoundException("entity jury was not found");
        }
        return new JuryModel()
                {
                    Name = jury.Name,
                    Surname = jury.Surname,
                    Birthday = jury.Birthday
                };
    }

    public async Task<JuryModel> UpdateAsync(JuryModel model, CancellationToken ct)
    {
        var juryList = await _repositoryBase.FindByConditionAsync(j => j.Id == model.Id, ct);
        Jury result = juryList.FirstOrDefault();

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
