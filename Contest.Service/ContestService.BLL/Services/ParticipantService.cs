using ContestService.BLL.Exceptions;
using ContestService.BLL.Interfaces;
using ContestService.BLL.Models;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Interfaces;
using System.Data;

namespace ContestService.BLL.Services;
public class ParticipantService(IRepositoryBase<Participant> repositoryBase,
                                    INominationRepository nominationRepository) : IParticipantService
{
    private readonly IRepositoryBase<Participant> _repositoryBase = repositoryBase;
    private readonly INominationRepository _nominationRepository = nominationRepository;

    public async Task<ParticipantModel> CreateParticipantAsync(ParticipantModel model, CancellationToken ct)
    {
        if (!_nominationRepository.IsMusicalInstrumentInNomination(model.NominationId, model.MusicalInstrumentId))
        {
            throw new BadRequestException("The selected musical instrument does not correspond to the specified category.");
        }
        Participant newParticipant = new Participant
        {
            Name = model.Name,
            Surname = model.Surname,
            Birthday = model.Birthday,
            MusicalInstrumentId = model.MusicalInstrumentId,
        };

        var createdEntity = await _repositoryBase.CreateAsync(newParticipant, ct);

        return new ParticipantModel 
        {
            Id = createdEntity.Id,
            Name = createdEntity.Name,
            Surname = createdEntity.Surname,
            Birthday = createdEntity.Birthday,
            MusicalInstrumentId= createdEntity.MusicalInstrumentId,
            NominationId = _nominationRepository.GetNominationIdByInstrumentId(createdEntity.MusicalInstrumentId),
        };
    }

    public async Task DeleteParticipantAsync(Guid id, CancellationToken ct)
    {
        var query = _repositoryBase.FindByCondition(p => p.Id == id, ct).FirstOrDefault();
        if (query != null)
        {
            await _repositoryBase.DeleteAsync(query, ct);
        }
        {
            throw new NotFoundException($"Participant with id {id} was not found");
        }
    }

    public async Task<List<ParticipantModel>> GetAllAsync(CancellationToken ct)
    {
        var entities = await _repositoryBase.GetAllToListAsync(ct);
        var model = entities.Select(e => new ParticipantModel
        {
            Id = e.Id,
            Name = e.Name,
            Surname = e.Surname,
            Birthday = e.Birthday,
            MusicalInstrumentId = e.MusicalInstrumentId,
            NominationId = _nominationRepository.GetNominationIdByInstrumentId(e.MusicalInstrumentId)
        }).ToList();

        return model;
    }

    public async Task<ParticipantModel> GetParticipantByIdAsync(Guid id, CancellationToken ct)
    {
        var resultList = await _repositoryBase.FindByConditionToListAsync(p => p.Id == id, ct);
        Participant result = resultList.FirstOrDefault();
        return result == null
            ? throw new NotFoundException("entity participant was not found")
            : new ParticipantModel()
            {
                Name = result.Name,
                Surname = result.Surname,
                Birthday = result.Birthday,
                MusicalInstrumentId = result.MusicalInstrumentId,
                NominationId = _nominationRepository.GetNominationIdByInstrumentId(result.MusicalInstrumentId)
            };
    }

    public async Task<ParticipantModel> UpdateParticipantAsync(ParticipantModel model, CancellationToken ct)
    {
        var query = await _repositoryBase.FindByConditionToListAsync(p => p.Id == model.Id, ct);
        Participant result = query.FirstOrDefault();

        result.Name = model.Name;
        result.Surname = model.Surname;
        result.Birthday = model.Birthday;
        result.MusicalInstrumentId = model.MusicalInstrumentId;

        var updatedParticipant = await _repositoryBase.UpdateAsync(result, ct);

        return new ParticipantModel
        {
            Name = updatedParticipant.Name,
            Surname = updatedParticipant.Surname,
            Birthday = updatedParticipant.Birthday,
            MusicalInstrumentId = updatedParticipant.MusicalInstrumentId,
            NominationId = _nominationRepository.GetNominationIdByInstrumentId(updatedParticipant.MusicalInstrumentId)
        };
    }
}
