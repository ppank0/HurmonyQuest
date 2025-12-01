using ContestService.DAL.Context;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Implementations;
using ContestService.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContestService.DAL.DI;

public static class DependencyInjection
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

        var redisSection = configuration.GetSection("RedisCacheSettings");

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisSection["ConnectionString"];
            options.InstanceName = redisSection["InstanceName"];
        });

        services.AddScoped<IRepositoryBase<Jury>, RepositoryBase<Jury>>();
        services.AddScoped<IRepositoryBase<MusicalInstrument>, RepositoryBase<MusicalInstrument>>();
        services.AddScoped<IRepositoryBase<Nomination>, RepositoryBase<Nomination>>();
        services.AddScoped<IRepositoryBase<Participant>, RepositoryBase<Participant>>();
        services.AddScoped<IRepositoryBase<Stage>, RepositoryBase<Stage>>();

        services.AddScoped<INominationRepository, NominationRepository>();
        services.AddScoped<IParticipantRepository, ParticipantRepository>();

        services.Decorate(typeof(IRepositoryBase<>), typeof(CachedRepositoryDecorator<>));

        return services;
    }
}
