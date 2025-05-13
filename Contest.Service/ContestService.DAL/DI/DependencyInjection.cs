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

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("RedisConnection");
            options.InstanceName = "ContestService_";
        });

        services.AddScoped<IRepositoryBase<Jury>, RepositoryBase<Jury>>();
        services.Decorate<IRepositoryBase<Jury>, CachedRepositoryDecorator<Jury>>();

        services.AddScoped<IRepositoryBase<MusicalInstrument>, RepositoryBase<MusicalInstrument>>();
        services.Decorate<IRepositoryBase<MusicalInstrument>, CachedRepositoryDecorator<MusicalInstrument>>();

        services.AddScoped<IRepositoryBase<Nomination>, RepositoryBase<Nomination>>();
        services.Decorate<IRepositoryBase<Nomination>, CachedRepositoryDecorator<Nomination>>();

        services.AddScoped<IRepositoryBase<Participant>, RepositoryBase<Participant>>();
        services.Decorate<IRepositoryBase<Participant>, CachedRepositoryDecorator<Participant>>();

        services.AddScoped<IRepositoryBase<Stage>, RepositoryBase<Stage>>();
        services.Decorate<IRepositoryBase<Stage>, CachedRepositoryDecorator<Stage>>();

        services.AddScoped<INominationRepository, NominationRepository>();

        return services;
    }
}
