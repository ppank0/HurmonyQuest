using ApplicationService.BLL.DI;

namespace Application.Service.DI
{
    public static class DI
    {
        public static IServiceCollection AddApiDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBLLDependencies(configuration);
            return services;
        }
    }
}
