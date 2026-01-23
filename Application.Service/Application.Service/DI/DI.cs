using Application.Service.Extensions;
using Application.Service.HttpClients;
using Application.Service.Mapper;
using Application.Service.Options;
using ApplicationService.BLL.DI;
using ApplicationService.Handlers;
using ApplicationService.BLL.Integrations.Contracts.Duende;
using ApplicationService.BLL.Integrations.Contracts.Instruments;
using ApplicationService.BLL.Integrations.Contracts.Participant;
using ApplicationService.BLL.Interfaces;
using Microsoft.Extensions.Options;
using ApplicationService.BLL.Integrations.Contracts.Auth;

namespace Application.Service.DI
{
    public static class DI
    {
        public static IServiceCollection AddApiDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(MapperProfile));
            services.AddBLLDependencies(configuration);
            services.Configure<ParticipantsOptions>(configuration.GetSection("Participants"));
            services.Configure<InstrumentsOptions>(configuration.GetSection("Instruments"));
            services.AddScoped<ITokenProvider, TokenProvider>();

            //services.AddTransient<AuthTokenHandler>(sp =>
            //    new AuthTokenHandler(sp.GetRequiredService<ITokenProvider>(), "Participant"));
            //services.AddTransient<AuthTokenHandler>(sp =>
            //    new AuthTokenHandler(sp.GetRequiredService<ITokenProvider>(), "Instrument"));

            services.AddHttpClient<IParticipantHttpClient, ParticipantHttpClient>((sp, http) =>
            {
                var opt = sp.GetRequiredService<IOptions<ParticipantsOptions>>().Value;
                http.BaseAddress = new Uri(opt.BaseUrl);
                http.Timeout = TimeSpan.FromSeconds(3);
                if (!string.IsNullOrWhiteSpace(opt.ApiKey))
                    http.DefaultRequestHeaders.Add("X-Api-Key", opt.ApiKey);
            }).AddHttpMessageHandler(sp =>
                new AuthTokenHandler(sp.GetRequiredService<ITokenProvider>(), "Participant"));

            services.AddHttpClient<IInstrumentHttpClient, InstrumentHttpClient>((sp, http) =>
            {
                var opt = sp.GetRequiredService<IOptions<InstrumentsOptions>>().Value;
                http.BaseAddress = new Uri(opt.BaseUrl);
                http.Timeout = TimeSpan.FromMinutes(2);
                if (!string.IsNullOrWhiteSpace(opt.ApiKey))
                    http.DefaultRequestHeaders.Add("X-Api-Key", opt.ApiKey);
            }).AddHttpMessageHandler(sp => 
                new AuthTokenHandler(sp.GetRequiredService<ITokenProvider>(), "Instrument"));

            services.AddHttpClient<IDuendeHttpClient, DuendeHttpClient>((sp, http) =>
            {
                http.BaseAddress = new Uri(configuration["DuendeIdentityServer:BaseUrl"]);
                http.Timeout = TimeSpan.FromMinutes(2);
            });

            services.AddCustomAuth(configuration);

            return services;
        }
    }
}
