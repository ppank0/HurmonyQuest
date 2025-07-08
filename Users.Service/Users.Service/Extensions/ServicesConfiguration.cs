using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using UsersService.Application;
using UsersService.Application.MapperProfile;
using UsersService.Infrastructure.DI;

namespace UsersService.API.Extensions
{
    public static class ServicesConfiguration
    {
        public static void AddServicesConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructure(configuration);
            services.AddAuth0Authentication(configuration);
            services.ConfigureMediatR();
            services.AddAutoMapper(typeof(UserProfile));
        }
        private static void AddAuth0Authentication(this IServiceCollection services, IConfiguration configuration)
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

        private static void ConfigureMediatR(this IServiceCollection services) =>
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationMarker).Assembly));

    }
}
