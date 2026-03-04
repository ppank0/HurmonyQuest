using ApplicationService.BLL.Interfaces;
using ApplicationService.BLL.Mapper;
using ApplicationService.BLL.Services;
using ApplicationService.DAL.DI;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using Minio.AspNetCore;
using AppService = ApplicationService.BLL.Services.ApplicationService;

namespace ApplicationService.BLL.DI
{
    public static class DI
    {
        public static MinioOptions? MinioOptions { get; set; }
        public static IServiceCollection AddBLLDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(MapperProfile));

            services.AddDalDependencies(configuration);
            services.AddScoped<IVideoService, VideoService>();
            services.AddScoped<IVideoStorage, MinioStorage>();
            services.AddScoped<IApplicationService, AppService>();

            MinioOptions = configuration.GetSection("Minio").Get<MinioOptions>();
            var rabbitmqUsername = configuration.GetRequiredSection("RabbitMq:UserName").Value;
            var rabbitmqPassword = configuration.GetRequiredSection("RabbitMq:Password").Value;

            services.AddSingleton<IMinioClient>(sp =>
                 new MinioClient()
                 .WithEndpoint(MinioOptions.Endpoint)
                 .WithCredentials(MinioOptions.AccessKey, MinioOptions.SecretKey)
                 .Build()
            );

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", host =>
                    {
                        host.Username(rabbitmqUsername!);
                        host.Password(rabbitmqPassword!);
                    });

                });
            });

            return services;
        }
    }
}
