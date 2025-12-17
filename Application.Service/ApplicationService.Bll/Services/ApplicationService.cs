using ApplicationService.BLL.Exeptions;
using ApplicationService.BLL.Integrations.Contracts.Instruments;
using ApplicationService.BLL.Integrations.Contracts.Instruments.DTOs;
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
    public class ApplicationService(IMapper mapper, IUnitOfWork _uOw,
        IParticipantHttpClient _participant, IInstrumentHttpClient _instrument, IVideoService _videoService) : IApplicationService
    {
        public async Task<ApplicationModel> CreateAsync(CreateApplicationRequest request, CancellationToken ct)
        {
            var participant = await _participant.CreateAsync((new ParticipantCreateRequest(request.Name, request.Surname, request.Birthday,
                                            request.MusicalInstrumentId, request.NominationId)), ct);
            var instrument = await _instrument.GetByIdAsync(request.MusicalInstrumentId, ct);

            var appEntity = new ApplicationEntity()
            {
                ParticipantId = participant.Id,
                Status = ApplicationStatus.RendingReview,
                NominationId = instrument.NominationId,
                InstrumentId = instrument.Id,
                VideoId = request.VideoId,
            };

            await _uOw.Applications.CreateAsync(appEntity, ct);
            await _uOw.SaveAsync(ct);

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
            var application = await _uOw.Applications.FindByCondition(x => x.Id == id, ct).FirstOrDefaultAsync(ct);

            if (application is null)
            {
                throw new NotFoundException($"Application with id: {id} was not found");
            }
            await _participant.DeleteAsync(application.ParticipantId, ct);
            await _videoService.DeleteAsync(application.VideoId, ct);

            await _uOw.SaveAsync(ct);
        }

        public async Task<List<ApplicationModel>> GetAllAsync(CancellationToken ct)
        {
            var applications = await _uOw.Applications.GetAllAsync(ct);

            if(applications.Count() == 0)
            {
                throw new NotFoundException("There are no applications");
            }

            return mapper.Map<List<ApplicationModel>>(applications);
        }

        public async Task<ApplicationModel> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var application = await _uOw.Applications.FindByCondition(x => x.Id == id, ct).FirstOrDefaultAsync(ct);
            if (application is null)
            {
                throw new NotFoundException($"Application with id: {id} was not found");
            }

            var additionalData = await GetDetailedInfoAsync(application, ct);
            return new ApplicationModel
            {
                Id = id,
                ParticipantName = additionalData.Item2.Name,
                ParticipantSurname = additionalData.Item2.Surname,
                ParticipantBirthday = additionalData.Item2.Birthday,
                InstrumentName = additionalData.Item1.Name,
                Status = application.Status,
            };
        }

        public async Task<ApplicationModel> UpdateAsync(UpdateApplicationRequest request, CancellationToken ct)
        {
            var application = await _uOw.Applications.FindByCondition(x => x.Id == request.id, ct).FirstOrDefaultAsync();
            if (application is null)
            {
                throw new NotFoundException($"Application with id: {request.id} was not found");
            }
            application.Status = request.status;
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
    }
}
