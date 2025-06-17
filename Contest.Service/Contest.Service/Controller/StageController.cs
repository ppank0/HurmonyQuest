using AutoMapper;
using ContestService.API.DTO.StageDtos;
using ContestService.BLL.Interfaces;
using ContestService.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContestService.API.Controller;

[Route("api/stages")]
[ApiController]
[Authorize(Roles = "Admin")]
public class StageController(IStageService stageService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "CanReadOnly")]
    public async Task<List<StageDto>> GetAll(CancellationToken ct)
    {
        var result = await stageService.GetAllAsync(ct);

        return mapper.Map<List<StageDto>>(result);
    }

    [HttpGet("{id}")]
    public async Task<StageDto> Get(Guid id, CancellationToken ct)
    {
        var result = await stageService.GetAsync(id, ct);

        return mapper.Map<StageModel, StageDto>(result);
    }

    [HttpPost]
    public async Task<StageDto> Create([FromBody] StageEditDto stageDto, CancellationToken ct)
    {
        var stageModel = mapper.Map<StageModel>(stageDto);
        var result = await stageService.CreateAsync(stageModel, ct);

        return mapper.Map<StageDto>(result);
    }

    [HttpPut("{id}")]
    public async Task<StageDto> Update(Guid id, [FromBody] StageEditDto stageDto, CancellationToken ct)
    {
        var stageModel = await stageService.GetAsync(id, ct);
        mapper.Map(stageDto, stageModel);

        var result = await stageService.UpdateAsync(stageModel, ct);

        return mapper.Map<StageDto>(result);
    }

    [HttpDelete("{id}")]
    public async Task Delete(Guid id, CancellationToken ct)
    {
        await stageService.DeleteAsync(id, ct);
    }
}
