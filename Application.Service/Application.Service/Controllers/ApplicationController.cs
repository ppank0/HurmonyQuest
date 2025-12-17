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
        public async Task<ApplicationDto> CreateAsync(CreateApplicationApiRequest request, CancellationToken ct)
        {
            await using var stream = request.File.OpenReadStream();
            var videoModel = await videoService.PutAsync(request.File.FileName, request.File.ContentType, stream, ct);

            var newApp = await applicationService.CreateAsync(new CreateApplicationRequest(request.Name, request.Surname,
                request.Birthday, request.MusicalInstrumentId, request.NominationId, videoModel.Id), ct);
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
        public async Task<ApplicationDto> UpdateStatusAsync(Guid id, [FromBody] ApplicationStatus status, CancellationToken ct)
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
