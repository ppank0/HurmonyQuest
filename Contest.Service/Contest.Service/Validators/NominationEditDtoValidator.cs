using ContestService.API.DTO.NominationDtos;
using ContestService.API.Extensions;
using FluentValidation;

namespace ContestService.API.Validators;

public class NominationEditDtoValidator : AbstractValidator<NominationEditDto>
{
    public NominationEditDtoValidator()
    {
        RuleFor(model => model.Name).CommonNameRules();
    }
}
