using AutoMapper;
using ContestService.API.DTO.MusicalInstrumentDtos;
using ContestService.BLL.Interfaces;
using ContestService.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContestService.API.Controller;

[Route("api/instruments")]
[ApiController]
[Authorize]
public class MusicalInstrumentController(IMusicalInstrumentService instrumentService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<List<MusicalInstrumentDto>> GetAll(CancellationToken ct)
    {
        var instruments = await instrumentService.GetAllAsync(ct);

        return mapper.Map<List<MusicalInstrumentDto>>(instruments);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<MusicalInstrumentDto> Get(Guid id, CancellationToken ct)
    {
        var instrument = await instrumentService.GetAsync(id, ct);

        return mapper.Map<MusicalInstrumentDto>(instrument);
    }

    [HttpPost]
    public async Task<MusicalInstrumentDto> Create([FromBody] MusicalInstrumentEditDto instrumentDto, CancellationToken ct)
    {
        var instrument = mapper.Map<MusicalInstrumentModel>(instrumentDto);
        var result = await instrumentService.CreateAsync(instrument, ct);

        return mapper.Map<MusicalInstrumentDto>(result);
    }

    [HttpPut("{id}")]
    public async Task<MusicalInstrumentDto> Update(Guid id, [FromBody] MusicalInstrumentEditDto instrumentDto, CancellationToken ct)
    {
        var instrumentModel = await instrumentService.GetAsync(id, ct);
        mapper.Map(instrumentDto, instrumentModel);

        var result = await instrumentService.UpdateAsync(instrumentModel, ct);

        return mapper.Map<MusicalInstrumentDto>(result);
    }

    [HttpDelete("{id}")]
    public async Task Delete(Guid id, CancellationToken ct)
    {
        await instrumentService.DeleteAsync(id, ct);
    }
}
