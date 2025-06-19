using ContestService.API.Authorization;
using ContestService.API.Extensions;
using ContestService.API.Mapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;

namespace ContestService.API.DI;

public static class DependencyInjection
{
    public static void AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddAutoValidation();
        services.AddAuth0Authentication(configuration);

        services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
        services.AddSwaggerDocumentation();
    }
    public static void AddAutoValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
    }

    public static void AddAuth0Authentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = $"https://{configuration["Auth0:Domain"]}/"; 
            options.Audience = configuration["Auth0:Audience"];
        });
    }
}
