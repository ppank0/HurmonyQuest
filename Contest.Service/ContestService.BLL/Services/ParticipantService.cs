using ContestService.BLL.Exceptions;
using ContestService.BLL.Interfaces;
using ContestService.BLL.Models;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Interfaces;
using System.Data;

namespace ContestService.BLL.Services;
public class ParticipantService(IRepositoryBase<Participant> _repositoryBase,
                                    INominationRepository _nominationRepository) : IParticipantService
{
    public async Task<ParticipantModel> CreateAsync(ParticipantModel model, CancellationToken ct)
    {
        if (!_nominationRepository.IsMusicalInstrumentInNomination(model.NominationId, model.MusicalInstrumentId))
        {
            throw new BadRequestException("The selected musical instrument does not correspond to the specified category.");
        }
        var newParticipant = new Participant
        {
            Name = model.Name,
            Surname = model.Surname,
            Birthday = model.Birthday,
            MusicalInstrumentId = model.MusicalInstrumentId,
        };

        var createdParticipant = await _repositoryBase.CreateAsync(newParticipant, ct);

        return new ParticipantModel 
        {
            Id = createdParticipant.Id,
            Name = createdParticipant.Name,
            Surname = createdParticipant.Surname,
            Birthday = createdParticipant.Birthday,
            MusicalInstrumentId= createdParticipant.MusicalInstrumentId,
            NominationId = _nominationRepository.GetNominationIdByInstrumentId(createdParticipant.MusicalInstrumentId),
        };
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var participant = _repositoryBase.FindByCondition(p => p.Id == id, ct).FirstOrDefault();
        if (participant is not null)
        {
            await _repositoryBase.DeleteAsync(participant, ct);
        }
        else
        {
            throw new NotFoundException($"Participant with id {id} was not found");
        }
    }

    public async Task<List<ParticipantModel>> GetAllAsync(CancellationToken ct)
    {
        var participantList = await _repositoryBase.GetAllToListAsync(ct);
        var model = participantList.Select(e => new ParticipantModel
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

    public async Task<ParticipantModel> GetAsync(Guid id, CancellationToken ct)
    {
        var participantList = await _repositoryBase.FindByConditionAsync(p => p.Id == id, ct);
        var participant = participantList.FirstOrDefault();

        if (participant is null)
        {
            throw new NotFoundException("entity participant was not found");
        } 

        return  new ParticipantModel()
                {
                    Name = participant.Name,
                    Surname = participant.Surname,
                    Birthday = participant.Birthday,
                    MusicalInstrumentId = participant.MusicalInstrumentId,
                    NominationId = _nominationRepository.GetNominationIdByInstrumentId(participant.MusicalInstrumentId)
                };
    }

    public async Task<ParticipantModel> UpdateAsync(ParticipantModel model, CancellationToken ct)
    {
        var participantList = await _repositoryBase.FindByConditionAsync(p => p.Id == model.Id, ct);
        var participant = participantList.FirstOrDefault();

        participant.Name = model.Name;
        participant.Surname = model.Surname;
        participant.Birthday = model.Birthday;
        participant.MusicalInstrumentId = model.MusicalInstrumentId;

        var updatedParticipant = await _repositoryBase.UpdateAsync(participant, ct);

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
