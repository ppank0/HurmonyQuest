using FluentValidation;
using System.Runtime.CompilerServices;

namespace ContestService.API.Extensions;

public static class ValidationExtension
{
    public static IRuleBuilderOptions<T, string> CommonNameRules<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder.NotEmpty().WithMessage("Name is required.")
            .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");
    }

    public static IRuleBuilderOptions<T, string> CommonSurnameRules<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder.NotEmpty().WithMessage("Surname is required.")
            .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");
    }

    public static IRuleBuilderOptions<T, DateOnly> CommonBirthdayRules<T>(this IRuleBuilder<T, DateOnly> ruleBuilder)
    {
        return ruleBuilder.NotNull().WithMessage("Birthday is required")
            .Must(birthday =>
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var age = today.Year - birthday.Year;
            if (birthday > today.AddYears(-age)) age--;
            return age >= 18 && age <= 120;
        })
            .WithMessage("Age must be between 18 and 120 years.");
    }

}
