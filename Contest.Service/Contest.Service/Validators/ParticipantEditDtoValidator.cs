using ContestService.API.DTO.ParticipantDtos;
using FluentValidation;

namespace ContestService.API.Validators;

public class ParticipantEditDtoValidator : AbstractValidator<ParticipantEditDto>
{
    public ParticipantEditDtoValidator()
    {
        RuleFor(model => model.Name).NotEmpty().WithMessage("Name is required.")
            .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");

        RuleFor(model => model.Surname).NotEmpty().WithMessage("Surname is required.")
            .Length(2, 50).WithMessage("Surname must be between 2 and 50 characters.");

        RuleFor(model => model.Birthday).Must(birthday =>
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var age = today.Year - birthday.Year;
            if (birthday > today.AddYears(-age)) age--;
            return age >= 18 && age <= 120;
        })
            .WithMessage("Age must be between 18 and 120 years.");
    }
}
