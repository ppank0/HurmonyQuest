using ContestService.API.Mapper;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Reflection;
using ContestService.API.Validators;

namespace ContestService.API.DI;

public static class DependencyInjection
{
    public static void AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddAutoValidation();

    }
    public static void AddAutoValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
    }
}
