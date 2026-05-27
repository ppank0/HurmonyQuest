using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using DuendeIdentityServer.Data;
using DuendeIdentityServer.Model;
using Microsoft.AspNetCore.Identity;

namespace DuendeIdentityServer
{
    public static class DataSeed
    {
        public static async Task SeedData(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

            await CreateClientIfNotExist(context);
            await CreateResourceIfNotExist(context);
            await CreateScopesIfNotExist(context);
            await SeedRoles(roleManager);

            await CreateUserIfNotExists(
                userManager,
                email: "admin@local",
                password: "Admin123!",
                roles: new[] { Roles.Admin }
            );

            await CreateUserIfNotExists(
                userManager,
                email: "jury@local",
                password: "Jury123!",
                roles: new[] { Roles.Jury }
            );

            await CreateUserIfNotExists(
                userManager,
                email: "participant@local",
                password: "Participant123!",
                roles: new[] { Roles.Participant }
            );
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(Roles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }
            if (!await roleManager.RoleExistsAsync(Roles.Jury))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Jury));
            }
            if (!await roleManager.RoleExistsAsync(Roles.Participant))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Participant));
            }
        }

        private static async Task CreateClientIfNotExist(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in Config.Clients)
                {
                    context.Clients.Add(client.ToEntity());
                }
            }
            context.SaveChanges();
        }
        private static async Task CreateScopesIfNotExist(ConfigurationDbContext context)
        {
            if (!context.ApiScopes.Any())
            {
                foreach (var scope in Config.ApiScopes)
                {
                    context.ApiScopes.Add(scope.ToEntity());
                }
            }
            context.SaveChanges();
        }

        private static async Task CreateResourceIfNotExist(ConfigurationDbContext context)
        {
            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.IdentityResources)
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
            }
            context.SaveChanges();
        }

        private static async Task CreateUserIfNotExists(UserManager<ApplicationUser> userManager,
                                                string email, string password, string[] roles)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user != null)
                return;

            user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new Exception(
                    $"Failed to create user {email}: " +
                    string.Join(", ", result.Errors.Select(e => e.Description))
                );
            }

            await userManager.AddToRolesAsync(user, roles);
        }
    }
}


