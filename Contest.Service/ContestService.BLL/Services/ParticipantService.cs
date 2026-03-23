using AutoMapper;
using ContestService.BLL.Exceptions;
using ContestService.BLL.Interfaces;
using ContestService.BLL.Models;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Interfaces;

namespace ContestService.BLL.Services;
public class ParticipantService(IParticipantRepository participantRepository,
        INominationRepository nominationRepository, IMusicalInstrumentService instrumentService,
        IMapper mapper) : IParticipantService
{
    public async Task<ParticipantModel> CreateAsync(ParticipantModel model, CancellationToken ct)
    {
        if (!nominationRepository.IsMusicalInstrumentInNomination(model.NominationId, model.MusicalInstrumentId))
        {
            throw new BadRequestException("The selected musical instrument does not correspond to the specified category.");
        }

        var newParticipant = mapper.Map<Participant>(model);
        var createdParticipant = await participantRepository.CreateAsync(newParticipant, ct); 
        var resultParticipant = mapper.Map<ParticipantModel>(createdParticipant);
        resultParticipant.NominationId = model.NominationId;

        return resultParticipant;
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var participants = await participantRepository.FindByConditionAsync(p => p.Id == id, ct);
        var participant = participants.FirstOrDefault();

        if (participant is not null)
        {
            await participantRepository.DeleteAsync(participant, ct);
        }
        else
        {
            throw new NotFoundException($"Participant with id {id} was not found");
        }
    }

    public async Task<List<ParticipantModel>> GetAllAsync(CancellationToken ct)
    {
        var participantList = await participantRepository.GetAllWithDetails(ct);
        return mapper.Map<List<ParticipantModel>>(participantList);
    }

    public async Task<ParticipantModel> GetAsync(Guid id, CancellationToken ct)
    {
        var participantList = await participantRepository.FindByConditionAsync(p => p.Id == id, ct);
        var participant = participantList.FirstOrDefault();
        if (participant is null)
        {
            throw new NotFoundException("entity participant was not found");
        }
        var additionalInfo = await instrumentService.GetAsync(participant.MusicalInstrumentId, ct);
        var participantModel = mapper.Map<ParticipantModel>(participant);

        participantModel.NominationId = additionalInfo.NominationId;
        participantModel.NominationName = additionalInfo.NominationName;

        return participantModel;
    }

    public async Task<ParticipantModel> UpdateAsync(ParticipantModel model, CancellationToken ct)
    {
        var participantList = await participantRepository.FindByConditionAsync(p => p.Id == model.Id, ct);
        var entityToUpdate = participantList.FirstOrDefault();

        if (entityToUpdate is null)
        {
            throw new NotFoundException($"{nameof(entityToUpdate)} is not found");
        }

        mapper.Map(model, entityToUpdate);
        var updatedParticipant = await participantRepository.UpdateAsync(entityToUpdate, ct);

        return mapper.Map<ParticipantModel>(updatedParticipant);
    }
}
