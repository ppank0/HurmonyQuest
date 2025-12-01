using ApplicationService.BLL.Interfaces;
using ApplicationService.BLL.Mapper;
using ApplicationService.BLL.Services;
using ApplicationService.DAL.DI;
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

            services.AddSingleton<IMinioClient>(sp =>
                 new MinioClient()
                 .WithEndpoint(MinioOptions.Endpoint)
                 .WithCredentials(MinioOptions.AccessKey, MinioOptions.SecretKey)
                 .Build()
            );

            return services;
        }
    }
}
