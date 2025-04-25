using ContestService.API.DTO.ParticipantDtos;
using ContestService.API.Extensions;
using FluentValidation;

namespace ContestService.API.Validators;

public class ParticipantEditDtoValidator : AbstractValidator<ParticipantEditDto>
{
    public ParticipantEditDtoValidator()
    {
        RuleFor(model => model.Name).CommonNameRules();

        RuleFor(model => model.Surname).CommonSurnameRules();

        RuleFor(model => model.Birthday).CommonBirthdayRules();
    }
}
