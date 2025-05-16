using ContestService.API.DTO.MusicalInstrumentDtos;
using ContestService.API.DTO.NominationDtos;
using ContestService.API.Extensions;
using FluentValidation;

namespace ContestService.API.Validators;

public class MusicalInstrumentEditDtoValidator : AbstractValidator<MusicalInstrumentEditDto>
{
    public MusicalInstrumentEditDtoValidator()
    {
        RuleFor(model => model.Name).CommonNameRules();
        RuleFor(model => model.NominationId).NotEmpty();
    }
}
