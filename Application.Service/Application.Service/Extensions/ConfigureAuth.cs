using Application.Service.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Service.Extensions
{
    public static class ConfigureAuth
    {
        public static void AddCustomAuth(this IServiceCollection services, IOptions<DuendeOptions> duendeOptions)
        {
            services.AddAuthentication()
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = duendeOptions.Value.BaseUrl;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = false, // Validate 
                        RoleClaimType = "role",
                    };
                    options.MapInboundClaims = false;
                });

            services.AddAuthorization(options =>
            {
                bool IsRole(AuthorizationHandlerContext ctx, string role)
                {
                    return ctx.User.IsInRole(role) || ctx.User.HasClaim("role", role);
                }

                bool HasScope(AuthorizationHandlerContext ctx, string scope)
                {
                     return ctx.User.Claims.Where(s => s.Type == "scope")
                            .SelectMany(v => v.Value.Split(" ", StringSplitOptions.RemoveEmptyEntries))
                            .Contains(scope);
                }

                options.AddPolicy("ReadApplications", policy => policy.RequireAssertion(ctx =>
                    (IsRole(ctx, "admin") || IsRole(ctx, "jury") || IsRole(ctx, "participant")) && HasScope(ctx, "application.read")
                ));

                options.AddPolicy("DeleteApplication", policy => policy.RequireAssertion(ctx =>
                    IsRole(ctx, "admin") && HasScope(ctx, "application.delete")
                ));

                options.AddPolicy("UpdateApplication", policy => policy.RequireAssertion(ctx =>
                    (IsRole(ctx, "admin") || IsRole(ctx, "jury")) && HasScope(ctx, "application.update")
                ));

                options.AddPolicy("CreateApplication", policy => policy.RequireAssertion(ctx =>
                    (IsRole(ctx, "admin") || IsRole(ctx, "participant")) && HasScope(ctx, "application.create")
                ));
            });
        }
    }
}
