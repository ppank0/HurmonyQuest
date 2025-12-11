using Application.Service.Dtos;
using ApplicationService.BLL.Interfaces;
using ApplicationService.BLL.Models.Requests;
using ApplicationService.DAL.Enum;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Application.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController(IMapper mapper, IVideoService videoService, IApplicationService applicationService) : ControllerBase
    {
        [HttpPost]
        public async Task<ApplicationDto> CreateAsync(string name, string surname, DateOnly birthday,
            Guid musicalInstrumentId, Guid nominationId, IFormFile file, CancellationToken ct)
        {
            await using var stream = file.OpenReadStream();
            var videoModel = await videoService.PutAsync(file.FileName, file.ContentType, stream, ct);

            var newApp = await applicationService.CreateAsync(new CreateApplicationRequest(name, surname, birthday, musicalInstrumentId,
                                    nominationId, videoModel.Id), ct);
            return mapper.Map<ApplicationDto>(newApp);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            await applicationService.DeleteAsync(id, ct);
        }

        [HttpGet("{id}")]
        public async Task<ApplicationDto> GetById(Guid id, CancellationToken ct)
        {
            return mapper.Map<ApplicationDto>(await applicationService.GetByIdAsync(id, ct));
        }

        [HttpPatch("{id}")]
        public async Task<ApplicationDto> UpdateStatusAsync(Guid id, ApplicationStatus status, CancellationToken ct)
        {
            return mapper.Map<ApplicationDto>(await applicationService.UpdateAsync(
                new UpdateApplicationRequest(id, status), ct));
        }

        [HttpGet]
        public async Task<List<ApplicationDto>> GetAll(CancellationToken ct)
        {
            return mapper.Map<List<ApplicationDto>>(await applicationService.GetAllAsync(ct));
        }
    }
}
