using AutoMapper;
using ContestService.API.DTO.NominationDtos;
using ContestService.BLL.Interfaces;
using ContestService.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContestService.API.Controller;

[Route("api/nominations")]
[ApiController]
public class NominationController(INominationService nominationService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<List<NominationDto>> GetAll(CancellationToken ct)
    {
        var result = await nominationService.GetAllAsync(ct);

        return mapper.Map<List<NominationDto>>(result);
    }

    [HttpGet("{id}")]
    public async Task<NominationDto> Get(Guid id, CancellationToken ct)
    {
        var result = await nominationService.GetAsync(id, ct);

        return mapper.Map<NominationModel, NominationDto>(result);
    }

    [HttpPost]
    public async Task<NominationDto> Create([FromBody] NominationEditDto nominationDto, CancellationToken ct)
    {
        var nominationModel = mapper.Map<NominationModel>(nominationDto);
        var result = await nominationService.CreateAsync(nominationModel, ct);

        return mapper.Map<NominationDto>(result);
    }

    [HttpPut("{id}")]
    public async Task<NominationDto> Update(Guid id, [FromBody] NominationEditDto nominationEditDto, CancellationToken ct)
    {
        var nominationModel = await nominationService.GetAsync(id, ct);
        mapper.Map(nominationEditDto, nominationModel);

        var result = await nominationService.UpdateAsync(nominationModel, ct);

        return mapper.Map<NominationDto>(result);
    }

    [HttpDelete("{id}")]
    public async Task Delete(Guid id, CancellationToken ct)
    {
        await nominationService.DeleteAsync(id, ct);
    }
}
