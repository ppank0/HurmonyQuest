using ContestService.DAL.Context;
using ContestService.DAL.Interceptors;
using ContestService.DAL.Repositories.Implementations;
using ContestService.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContestService.DAL.DI
{
    public  static class DependencyInjection
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

            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

            return services;

        }
    }
}
