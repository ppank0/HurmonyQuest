using ContestService.API.DTO.StageDtos;
using ContestService.API.Extensions;
using FluentValidation;

namespace ContestService.API.Validators;

public class StageEditDtoValidation : AbstractValidator<StageEditDto>
{
    public StageEditDtoValidation()
    {
        RuleFor(stage => stage.Name).CommonNameRules();
        RuleFor(stage => stage.StartDate)
                .LessThanOrEqualTo(stage => stage.EndDate)
                .WithMessage("The stage start date cannot be later than the end date.");

        RuleFor(stage => stage.EndDate)
                .GreaterThanOrEqualTo(stage => stage.StartDate)
                .WithMessage("The stage end date cannot be earlier than the start date.");
    }
}
