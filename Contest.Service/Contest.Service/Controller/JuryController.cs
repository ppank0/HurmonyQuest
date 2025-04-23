using AutoMapper;
using ContestService.API.DTO.JuryDtos;
using ContestService.BLL.Interfaces;
using ContestService.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContestService.API.Controller;
[Route("api/[controller]")]
[ApiController]
public class JuryController(IMapper mapper, IJuryService juryService) : ControllerBase
{
    [HttpGet]
    public async Task<List<JuryDto>> GetAll(CancellationToken ct)
    {
        var juries = await juryService.GetAllAsync(ct);

        return mapper.Map<List<JuryDto>>(juries);
    }

    [HttpGet("{id}")]
    public async Task<JuryDto> Get(Guid id, CancellationToken ct)
    {
        var jury = await juryService.GetAsync(id, ct);

        return mapper.Map<JuryDto>(jury);
    }

    [HttpPost]
    public async Task<JuryEditDto> Create([FromForm] JuryEditDto juryDto, CancellationToken ct)
    {
        var jury = mapper.Map<JuryModel>(juryDto);
        var result = await juryService.CreateAsync(jury, ct);

        return mapper.Map<JuryEditDto>(result);
    }

    [HttpPut("{id}")]
    public async Task<JuryEditDto> Update(Guid id, JuryEditDto juryDto, CancellationToken ct)
    {
        var juryModel = await juryService.GetAsync(id, ct);
        mapper.Map(juryDto, juryModel);

        var result = await juryService.UpdateAsync(juryModel, ct);

        return mapper.Map<JuryEditDto>(result);
    }

    [HttpDelete("{id}")]
    public async Task Delete(Guid id, CancellationToken ct)
    {
        await juryService.DeleteAsync(id, ct);
    }
}
