using AutoMapper;
using ContestService.API.DTO.ParticipantDtos;
using ContestService.BLL.Interfaces;
using ContestService.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContestService.API.Controller;

[Route("api/participants")]
[ApiController]
[Authorize]
public class ParpicipantController(IParticipantService participantService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<List<ParticipantDto>> GetAll(CancellationToken ct)
    {
        var result = await participantService.GetAllAsync(ct);

        return mapper.Map<List<ParticipantDto>>(result);
    }

    [HttpGet("{id}")]
    public async Task<ParticipantDto> Get(Guid id, CancellationToken ct)
    {
        var result = await participantService.GetAsync(id, ct);

        return mapper.Map<ParticipantModel, ParticipantDto>(result);
    }

    [HttpPost]
    public async Task<ParticipantDto> Create([FromBody] ParticipantEditDto participantDto, CancellationToken ct)
    {
        var participantModel = mapper.Map<ParticipantModel>(participantDto);
        var result = await participantService.CreateAsync(participantModel, ct);

        return mapper.Map<ParticipantDto>(result);
    }

    [HttpPut("{id}")]
    [Authorize("edit:participant-edit-own")]
    public async Task<ParticipantDto> Update(Guid id, [FromBody] ParticipantEditDto participantDto, CancellationToken ct)
    {
        var participantModel = await participantService.GetAsync(id, ct);
        mapper.Map(participantDto, participantModel);

        var result = await participantService.UpdateAsync(participantModel, ct);

        return mapper.Map<ParticipantDto>(result);
    }

    [HttpDelete("{id}")]
    public async Task Delete(Guid id, CancellationToken ct)
    {
        await participantService.DeleteAsync(id, ct);
    }
}
