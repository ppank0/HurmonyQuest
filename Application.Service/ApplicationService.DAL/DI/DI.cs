using ApplicationService.DAL.Context;
using ApplicationService.DAL.Entities;
using ApplicationService.DAL.Repositories.Implementations;
using ApplicationService.DAL.Repositories.Interfaces;
using ApplicationService.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationService.DAL.DI
{
    public static class DI
    {
        public static IServiceCollection AddDalDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(connectionString, npgsqlOptions =>
                        npgsqlOptions.EnableRetryOnFailure(
                                        maxRetryCount: 5,
                                        maxRetryDelay: TimeSpan.FromSeconds(10),
                                        errorCodesToAdd: null)));

            services.AddScoped<IBaseRepository<ApplicationEntity>, BaseRepository<ApplicationEntity>>();
            services.AddScoped<IBaseRepository<VideoEntity>, BaseRepository<VideoEntity>>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            return services;
        }
    }
}
