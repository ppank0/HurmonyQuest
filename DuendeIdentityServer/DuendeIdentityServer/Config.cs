using Duende.IdentityModel;
using Duende.IdentityServer.Models;

namespace DuendeIdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
                { 
                    new ApiScope("application.read", "read application data"),
                    new ApiScope("application.create", "create application"),
                    new ApiScope("application.delete", "delete application"),
                    new ApiScope("application.edit", "edit application"),
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
                    }
                };
    }
}
