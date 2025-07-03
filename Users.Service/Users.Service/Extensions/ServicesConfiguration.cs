using Microsoft.EntityFrameworkCore;
using UsersService.Infrastructure.Context;

namespace UsersService.API.Extensions
{
    public static class ServicesConfiguration
    {
        public static void CongigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<UsersDBContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("UsersService.Infrastructure")));
    }
}
