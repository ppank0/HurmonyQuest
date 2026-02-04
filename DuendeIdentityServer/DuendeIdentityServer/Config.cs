using Duende.IdentityModel;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace DuendeIdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource()
                {
                    Name = "roles",
                    UserClaims = new List<string>
                    {
                        JwtClaimTypes.Role
                    }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
                {
                    new ApiScope("application.read", "read application data"),
                    new ApiScope("application.create", "create application"),
                    new ApiScope("application.delete", "delete application"),
                    new ApiScope("application.edit", "edit application"),
                    new ApiScope("participant.read", "read participant" ),
                    new ApiScope("instrument.read", "read instrument")
                };

        public static IEnumerable<Client> Clients =>
            new Client[]
                {
                    new Client
                    {
                        ClientId = "123",

                        AllowedGrantTypes = GrantTypes.ClientCredentials,

                        ClientSecrets = { new Secret ("123".ToSha256()) },

                        AllowedScopes = { "application.read" }

                    },
                    new Client
                    {
                        ClientId = "application_service",

                        AllowedGrantTypes = GrantTypes.ClientCredentials,

                        ClientSecrets = { new Secret ("ClientSecret123".ToSha256()) },

                        AllowedScopes = {"participant.read", "participant.create", "instrument.read"}

                    },
                    new Client
                    {
                        ClientId = "application_service.api",
                        ClientSecrets = { new Secret("ClientSecret123".Sha256()) },

                        AllowedGrantTypes = GrantTypes.Code,

                        RedirectUris = { "https://localhost:7225/swagger/oauth2-redirect.html",
                                         "https://oauth.pstmn.io/v1/callback"},

                        AllowedCorsOrigins = {"https://localhost:7225"},
                        PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                        AllowedScopes =
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            "application.read",
                            "application.create",
                            "application.delete",
                            "application.edit",
                            "roles"
                        }
                    }
                };
    }
}
