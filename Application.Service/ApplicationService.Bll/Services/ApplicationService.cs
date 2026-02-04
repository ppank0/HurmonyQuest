using ApplicationService.BLL.Exeptions;
using ApplicationService.BLL.Integrations.Contracts.Instruments;
using ApplicationService.BLL.Integrations.Contracts.Participant;
using ApplicationService.BLL.Integrations.Contracts.Participants.DTOs;
using ApplicationService.BLL.Interfaces;
using ApplicationService.BLL.Models;
using ApplicationService.BLL.Models.Requests;
using ApplicationService.DAL.Entities;
using ApplicationService.DAL.Enum;
using ApplicationService.DAL.UnitOfWork;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApplicationService.BLL.Services
{
    public class ApplicationService(IMapper mapper, IUnitOfWork unitOfWork,
        IParticipantHttpClient participantClient, IInstrumentHttpClient instrumentClient, IVideoService videoService) : IApplicationService
    {
        public async Task<ApplicationModel> CreateAsync(CreateApplicationRequest request, CancellationToken ct)
        {
            var participant = await participantClient.CreateAsync(mapper.Map<ParticipantCreateRequest>(request), ct);
            var instrument = await instrumentClient.GetByIdAsync(request.MusicalInstrumentId, ct);

            var appEntity = new ApplicationEntity()
            {
                ParticipantId = participant.Id,
                Status = ApplicationStatus.RendingReview,
                NominationId = instrument.NominationId,
                InstrumentId = instrument.Id,
                VideoId = request.VideoId,
            };

            await unitOfWork.Applications.CreateAsync(appEntity, ct);
            await unitOfWork.SaveAsync(ct);

            return new ApplicationModel()
            {
                Id = appEntity.Id,
                ParticipantName = request.Name,
                ParticipantSurname = request.Surname,
                ParticipantBirthday = request.Birthday,
                InstrumentName = instrument.Name,
                NominationName = request.Name,
                Status = appEntity.Status
            };
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var application = await unitOfWork.Applications.FindByCondition(x => x.Id == id, ct).FirstOrDefaultAsync(ct);

            if (application is null)
            {
                throw new NotFoundException($"Application with id: {id} was not found");
            }
            await participantClient.DeleteAsync(application.ParticipantId, ct);
            await videoService.DeleteAsync(application.VideoId, ct);

            await unitOfWork.SaveAsync(ct);
        }

        public async Task<List<ApplicationModel>> GetAllAsync(CancellationToken ct)
        {
            var applications = await unitOfWork.Applications.GetAllAsync(ct);

            if (applications.Count() == 0)
            {
                throw new NotFoundException("There are no applications");
            }

            return mapper.Map<List<ApplicationModel>>(applications);
        }

        public async Task<ApplicationModel> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var application = await unitOfWork.Applications.FindByCondition(x => x.Id == id, ct).FirstOrDefaultAsync(ct);
            if (application is null)
            {
                throw new NotFoundException($"Application with id: {id} was not found");
            }

            var participant = await participantClient.GetAsync(application.ParticipantId, ct);
            var instrument = await instrumentClient.GetByIdAsync(application.InstrumentId, ct);

            return new ApplicationModel
            {
                Id = id,
                ParticipantName = participant.Name,
                ParticipantSurname = participant.Surname,
                ParticipantBirthday = participant.Birthday,
                InstrumentName = instrument.Name,
                Status = application.Status,
            };
        }

        public async Task<ApplicationModel> UpdateAsync(UpdateApplicationRequest request, CancellationToken ct)
        {
            var application = await unitOfWork.Applications.FindByCondition(x => x.Id == request.id, ct).FirstOrDefaultAsync();
            if (application is null)
            {
                throw new NotFoundException($"Application with id: {request.id} was not found");
            }
            application.Status = request.status;
            await unitOfWork.Applications.UpdateAsync(application, ct);
            await unitOfWork.SaveAsync(ct);

            return await GetByIdAsync(request.id, ct);
        }
    }
}
