using Duende.IdentityModel;
using Duende.IdentityServer.AspNetIdentity;
using Duende.IdentityServer.Models;
using DuendeIdentityServer.Model;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DuendeIdentityServer.Services
{
    public class ProfileService : ProfileService<ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ProfileService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory) : base(userManager, claimsFactory)
        {
            _userManager = userManager;
        }

        public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            if (user is null) return;

            var claims = new List<Claim> {
                new Claim(JwtClaimTypes.Subject, user.Id)
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(JwtClaimTypes.Role, role)));

            context.IssuedClaims.AddRange(claims);
        }
    }
}
