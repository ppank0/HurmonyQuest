using ApplicationService.BLL.Exeptions;
using ApplicationService.BLL.Integrations.Contracts.Instruments;
<<<<<<< feature/9-create-integration-tests
using ApplicationService.BLL.Integrations.Contracts.Instruments.DTOs;
=======
>>>>>>> main
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
<<<<<<< feature/9-create-integration-tests
    public class ApplicationService(IMapper mapper, IUnitOfWork _uOw,
        IParticipantHttpClient _participant, IInstrumentHttpClient _instrument, IVideoService _videoService) : IApplicationService
    {
        public async Task<ApplicationModel> CreateAsync(CreateApplicationRequest request, CancellationToken ct)
        {
            var participant = await _participant.CreateAsync((new ParticipantCreateRequest(request.Name, request.Surname, request.Birthday,
                                            request.MusicalInstrumentId, request.NominationId)), ct);
            var instrument = await _instrument.GetByIdAsync(request.MusicalInstrumentId, ct);
=======
    public class ApplicationService(IMapper mapper, IUnitOfWork unitOfWork,
        IParticipantHttpClient participantClient, IInstrumentHttpClient instrumentClient, IVideoService videoService) : IApplicationService
    {
        public async Task<ApplicationModel> CreateAsync(CreateApplicationRequest request, CancellationToken ct)
        {
            var participant = await participantClient.CreateAsync(mapper.Map<ParticipantCreateRequest>(request), ct);
            var instrument = await instrumentClient.GetByIdAsync(request.MusicalInstrumentId, ct);
>>>>>>> main

            var appEntity = new ApplicationEntity()
            {
                ParticipantId = participant.Id,
                Status = ApplicationStatus.RendingReview,
                NominationId = instrument.NominationId,
                InstrumentId = instrument.Id,
                VideoId = request.VideoId,
            };

<<<<<<< feature/9-create-integration-tests
            await _uOw.Applications.CreateAsync(appEntity, ct);
            await _uOw.SaveAsync(ct);
=======
            await unitOfWork.Applications.CreateAsync(appEntity, ct);
            await unitOfWork.SaveAsync(ct);
>>>>>>> main

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
<<<<<<< feature/9-create-integration-tests
            var application = await _uOw.Applications.FindByCondition(x => x.Id == id, ct).FirstOrDefaultAsync(ct);
=======
            var application = await unitOfWork.Applications.FindByCondition(x => x.Id == id, ct).FirstOrDefaultAsync(ct);
>>>>>>> main

            if (application is null)
            {
                throw new NotFoundException($"Application with id: {id} was not found");
            }
<<<<<<< feature/9-create-integration-tests
            await _participant.DeleteAsync(application.ParticipantId, ct);
            await _videoService.DeleteAsync(application.VideoId, ct);

            await _uOw.SaveAsync(ct);
=======
            await participantClient.DeleteAsync(application.ParticipantId, ct);
            await videoService.DeleteAsync(application.VideoId, ct);

            await unitOfWork.SaveAsync(ct);
>>>>>>> main
        }

        public async Task<List<ApplicationModel>> GetAllAsync(CancellationToken ct)
        {
<<<<<<< feature/9-create-integration-tests
            var applications = await _uOw.Applications.GetAllAsync(ct);

            if(applications.Count() == 0)
=======
            var applications = await unitOfWork.Applications.GetAllAsync(ct);

            if (applications.Count() == 0)
>>>>>>> main
            {
                throw new NotFoundException("There are no applications");
            }

            return mapper.Map<List<ApplicationModel>>(applications);
        }

        public async Task<ApplicationModel> GetByIdAsync(Guid id, CancellationToken ct)
        {
<<<<<<< feature/9-create-integration-tests
            var application = await _uOw.Applications.FindByCondition(x => x.Id == id, ct).FirstOrDefaultAsync(ct);
=======
            var application = await unitOfWork.Applications.FindByCondition(x => x.Id == id, ct).FirstOrDefaultAsync(ct);
>>>>>>> main
            if (application is null)
            {
                throw new NotFoundException($"Application with id: {id} was not found");
            }

<<<<<<< feature/9-create-integration-tests
            var additionalData = await GetDetailedInfoAsync(application, ct);
            return new ApplicationModel
            {
                Id = id,
                ParticipantName = additionalData.Item2.Name,
                ParticipantSurname = additionalData.Item2.Surname,
                ParticipantBirthday = additionalData.Item2.Birthday,
                InstrumentName = additionalData.Item1.Name,
=======
            var instrument = await instrumentClient.GetByIdAsync(application.InstrumentId, ct);
            var participant = await participantClient.GetAsync(application.ParticipantId, ct);

            return new ApplicationModel
            {
                Id = id,
                ParticipantName = participant.Name,
                ParticipantSurname = participant.Surname,
                ParticipantBirthday = participant.Birthday,
                InstrumentName = instrument.Name,
>>>>>>> main
                Status = application.Status,
            };
        }

        public async Task<ApplicationModel> UpdateAsync(UpdateApplicationRequest request, CancellationToken ct)
        {
<<<<<<< feature/9-create-integration-tests
            var application = await _uOw.Applications.FindByCondition(x => x.Id == request.id, ct).FirstOrDefaultAsync();
=======
            var application = await unitOfWork.Applications.FindByCondition(x => x.Id == request.id, ct).FirstOrDefaultAsync();
>>>>>>> main
            if (application is null)
            {
                throw new NotFoundException($"Application with id: {request.id} was not found");
            }
            application.Status = request.status;
<<<<<<< feature/9-create-integration-tests
            await _uOw.Applications.UpdateAsync(application, ct);
            await _uOw.SaveAsync(ct);

            return await GetByIdAsync(request.id, ct);
        }

        private async Task<(InstrumentResponse, ParticipantResponse)> GetDetailedInfoAsync(ApplicationEntity app, CancellationToken ct)
        {
            var instrument = await _instrument.GetByIdAsync(app.InstrumentId, ct);
            var participant = await _participant.GetAsync(app.ParticipantId, ct);

            return (instrument, participant);
        }
=======
            await unitOfWork.Applications.UpdateAsync(application, ct);
            await unitOfWork.SaveAsync(ct);

            return await GetByIdAsync(request.id, ct);
        }
>>>>>>> main
    }
}
