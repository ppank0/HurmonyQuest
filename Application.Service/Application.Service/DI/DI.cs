using Application.Service.HttpClients;
using Application.Service.Mapper;
using Application.Service.Options;
using ApplicationService.BLL.DI;
using ApplicationService.BLL.Integrations.Contracts.Instruments;
using ApplicationService.BLL.Integrations.Contracts.Participant;
using Microsoft.Extensions.Options;

namespace Application.Service.DI
{
    public static class DI
    {
        public static IServiceCollection AddApiDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(MapperProfile));
            services.AddBLLDependencies(configuration);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationService.BLL.DI.DI).Assembly));
            services.Configure<ParticipantsOptions>(configuration.GetSection("Participants"));
            services.Configure<InstrumentsOptions>(configuration.GetSection("Instruments"));

            services.AddHttpClient<IParticipantHttpClient, ParticipantHttpClient>((sp, http) =>
            {
                var opt = sp.GetRequiredService<IOptions<ParticipantsOptions>>().Value;
                http.BaseAddress = new Uri(opt.BaseUrl);
                http.Timeout = Timeout.InfiniteTimeSpan;
                if (!string.IsNullOrWhiteSpace(opt.ApiKey))
                    http.DefaultRequestHeaders.Add("X-Api-Key", opt.ApiKey);
            });

            services.AddHttpClient<IInstrumentHttpClient, InstrumentHttpClient>((sp, http) =>
            {
                var opt = sp.GetRequiredService<IOptions<InstrumentsOptions>>().Value;
                http.BaseAddress = new Uri(opt.BaseUrl);
                http.Timeout = Timeout.InfiniteTimeSpan;
                if (!string.IsNullOrWhiteSpace(opt.ApiKey))
                    http.DefaultRequestHeaders.Add("X-Api-Key", opt.ApiKey);
            });

            return services;
        }
    }
}
