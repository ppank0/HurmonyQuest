using AutoMapper;
using ContestService.BLL.Exceptions;
using ContestService.BLL.Interfaces;
using ContestService.BLL.Models;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Interfaces;
using System.Data;

namespace ContestService.BLL.Services;
public class ParticipantService(IRepositoryBase<Participant> repository,
                                    INominationRepository nominationRepository, IMapper mapper) : IParticipantService
{
    public async Task<ParticipantModel> CreateAsync(ParticipantModel model, CancellationToken ct)
    {
        if (!nominationRepository.IsMusicalInstrumentInNomination(model.NominationId, model.MusicalInstrumentId))
        {
            throw new BadRequestException("The selected musical instrument does not correspond to the specified category.");
        }

        var newParticipant = mapper.Map<Participant>(model);
        var createdParticipant = await repository.CreateAsync(newParticipant, ct);

        return mapper.Map<ParticipantModel>(createdParticipant);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var participant = repository.FindByCondition(p => p.Id == id, ct).FirstOrDefault();
        if (participant is not null)
        {
            await repository.DeleteAsync(participant, ct);
        }
        else
        {
            throw new NotFoundException($"Participant with id {id} was not found");
        }
    }

    public async Task<List<ParticipantModel>> GetAllAsync(CancellationToken ct)
    {
        var participantList = await repository.GetAllToListAsync(ct);

        return mapper.Map<List<ParticipantModel>> (participantList);
    }

    public async Task<ParticipantModel> GetAsync(Guid id, CancellationToken ct)
    {
        var participantList = await repository.FindByConditionAsync(p => p.Id == id, ct);
        var participant = participantList.FirstOrDefault();

        if (participant is null)
        {
            throw new NotFoundException("entity participant was not found");
        }

        return mapper.Map<ParticipantModel>(participant);
    }

    public async Task<ParticipantModel> UpdateAsync(ParticipantModel model, CancellationToken ct)
    {
        var participantList = await repository.FindByConditionAsync(p => p.Id == model.Id, ct);
        var participant = participantList.FirstOrDefault();

        if (participant is null)
        {
            throw new NotFoundException($"{nameof(participant)} is not found");
        }

        participant.Name = model.Name;
        participant.Surname = model.Surname;
        participant.Birthday = model.Birthday;
        participant.MusicalInstrumentId = model.MusicalInstrumentId;

        var updatedParticipant = await repository.UpdateAsync(participant, ct);

        return mapper.Map<ParticipantModel>(updatedParticipant);
    }
}
