using ContestService.BLL.Interfaces;
using ContestService.BLL.Mapper;
using ContestService.BLL.Services;
using ContestService.DAL.DI;
using ContestService.DAL.Repositories.Implementations;
using ContestService.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContestService.BLL.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddBllServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDalDependencies(configuration);
        services.AddScoped<INominationService, NominationService>();
        services.AddScoped<IJuryService, JuryService>();
        services.AddScoped<IParticipantService, ParticipantService>();
        services.AddScoped<IMusicalInstrumentService, MusicalInstrumentService>();
        services.AddScoped<IStageService, StageService>();
        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}
