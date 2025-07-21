using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UsersService.Application;
using UsersService.Application.MapperProfile;
using UsersService.Domain.Interfaces;
using UsersService.Infrastructure.Context;
using UsersService.Infrastructure.Repositories;

namespace UsersService.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UsersDBContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly("UsersService.Infrastructure")));

            services.AddScoped<IUserRepository, UserRepository>();
            
            return services;
        }
    }
}
