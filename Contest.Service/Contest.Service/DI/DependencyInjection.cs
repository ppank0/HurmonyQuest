using ContestService.API.Extensions;
using ContestService.API.Mapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace ContestService.API.DI;

public static class DependencyInjection
{
    public static void AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddAutoValidation();
        services.AddDuendeAuthentication(configuration);

        services.AddSwaggerDocumentation();
    }
    public static void AddAutoValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
    }

    public static void AddDuendeAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(opt =>
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(opt =>
            {
                opt.Authority = configuration["DuendeServer:BaseUrl"];
                opt.RequireHttpsMetadata = false;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,

                };
            });

        bool HasScope(AuthorizationHandlerContext ctx, string scope)
        {
            return ctx.User.Claims.Where(c => c.Type == "scope")
                .SelectMany(v => v.Value.Split(" ", StringSplitOptions.RemoveEmptyEntries))
                .Contains(scope);
        };

        services.AddAuthorization(opt =>
        {
            opt.AddPolicy("CreateParticipant", policy =>
            {
                policy.RequireAssertion(context => HasScope(context, "participant.create"));
            });
        });

    }
}
