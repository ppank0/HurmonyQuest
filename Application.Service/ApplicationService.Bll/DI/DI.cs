using ApplicationService.DAL.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationService.BLL.DI
{
    public static class DI
    {
        public static IServiceCollection AddBLLDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDalDependencies(configuration);
            return services;
        }
    }
}
