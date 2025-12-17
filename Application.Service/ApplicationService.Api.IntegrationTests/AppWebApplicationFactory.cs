using Application.Service;
using ApplicationService.BLL.Integrations.Contracts.Instruments;
using ApplicationService.BLL.Integrations.Contracts.Participant;
using ApplicationService.BLL.Interfaces;
using ApplicationService.DAL.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Testcontainers.PostgreSql;

namespace ApplicationService.Api.IntegrationTests
{
    public class AppWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        public Mock<IInstrumentHttpClient> InstrumentMock { get; set; }
        public Mock<IParticipantHttpClient> ParticipantMock { get; set; }
        public Mock<IVideoService> VideoServiceMock { get; set; }

        private readonly PostgreSqlContainer testContariner = new PostgreSqlBuilder()
            .WithImage("postgres:16")
            .WithDatabase("appDb")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .WithExposedPort(5432)
            .WithCleanUp(true)
            .WithAutoRemove(true)
            .Build();

        public AppWebApplicationFactory()
        {
            InstrumentMock = new Mock<IInstrumentHttpClient>();
            ParticipantMock = new Mock<IParticipantHttpClient>();
            VideoServiceMock = new Mock<IVideoService>();
        }
        public Task InitializeAsync()
        {
            return testContariner.StartAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<AppDbContext>));

                services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(testContariner.GetConnectionString())
                );

                RemoveService<IParticipantHttpClient>(services);
                RemoveService<IInstrumentHttpClient>(services);
                RemoveService<IVideoService>(services);

                services.AddSingleton(ParticipantMock.Object);
                services.AddSingleton(InstrumentMock.Object);
                services.AddSingleton(VideoServiceMock.Object);
            });
        }
       
        public new async Task DisposeAsync()
        {
            await testContariner.DisposeAsync();
            await base.DisposeAsync();
        }

        private static void RemoveService<T>(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(T));
            if(descriptor is not null)
            {
                services.Remove(descriptor);
            }
        }
    }
}
