using ContestService.API.DTO.NominationDtos;
using FluentValidation;

namespace ContestService.API.Validators;

public class NominationEditDtoValidator : AbstractValidator<NominationEditDto>
{
    public NominationEditDtoValidator()
    {
        RuleFor(model => model.Name).NotEmpty().WithMessage("Nomination name is required")
            .Length(2, 50);
    }
}
