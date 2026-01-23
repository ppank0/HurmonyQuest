using ApplicationService.BLL.Integrations.Contracts.Duende;
using ApplicationService.BLL.Interfaces;
using ApplicationService.BLL.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ApplicationService.BLL.Integrations.Contracts.Auth
{
    public class TokenProvider(ITokenCache cache, IDuendeHttpClient duendeHttpClient, IConfiguration configuration) : ITokenProvider
    {
        public async Task<string> GetAccessTokenAsync(string downstream, CancellationToken ct)
        {
            var scopes = configuration[$"DuendeIdentityServer:Downstream:{downstream}:Scopes"];
            var key = BuildKey(downstream, scopes!);
            var token = await cache.GetTokenAsync(key, ct);
            if (token != null)
            {
                return token;
            }

            var tokenResponse = await duendeHttpClient.GetTokenAsync(downstream, ct);
            await cache.SetTokenAsync(key, tokenResponse, ct);

            return tokenResponse.AccessToken;
        }

        private string BuildKey(string downstream, string scopes)
        {
            return $"auth:token:{downstream}:{scopes}:";
        }
    }
}
