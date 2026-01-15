using Application.Service.Dtos;
using ApplicationService.BLL.Interfaces;
using ApplicationService.BLL.Models.Requests;
using ApplicationService.DAL.Enum;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApplicationController(IMapper mapper, IVideoService videoService, IApplicationService applicationService) : ControllerBase
    {
        [HttpPost]
        [Authorize(Policy = "CreateApplication")]
        public async Task<ApplicationDto> CreateAsync(CreateApplicationApiRequest request, CancellationToken ct)
        {
            await using var stream = request.File.OpenReadStream();
            var videoModel = await videoService.PutAsync(request.File.FileName, request.File.ContentType, stream, ct);
            var createApp = mapper.Map<CreateApplicationRequest>(request) with { VideoId = videoModel.Id };
            var newApp = await applicationService.CreateAsync(createApp, ct);

            return mapper.Map<ApplicationDto>(newApp);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            await applicationService.DeleteAsync(id, ct);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadApplication")]
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
