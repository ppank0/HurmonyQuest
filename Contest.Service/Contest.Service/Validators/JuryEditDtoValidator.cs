using ContestService.API.DTO.JuryDtos;
using ContestService.API.Extensions;
using FluentValidation;

namespace ContestService.API.Validators;

public class JuryEditDtoValidator : AbstractValidator<JuryEditDto>
{
    public JuryEditDtoValidator()
    {
        RuleFor(model => model.Name).CommonNameRules();

        RuleFor(model => model.Surname).CommonSurnameRules();

        RuleFor(model => model.Birthday).CommonBirthdayRules();
    }
}
