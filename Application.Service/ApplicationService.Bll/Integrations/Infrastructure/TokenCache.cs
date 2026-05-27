using ApplicationService.BLL.Integrations.Contracts.Auth.DTO;
using ApplicationService.BLL.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace ApplicationService.Bll.Integrations.Infractructure
{
    public class TokenCache(IDistributedCache cache) : ITokenCache
    {
        public async Task<string> GetTokenAsync(string key, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            return await cache.GetStringAsync(key, cancellationToken);
        }

        public async Task SetTokenAsync(string key, TokenResponse token, CancellationToken cancellationToken)
        {
            var ttl = token.ExpiresIn - Random.Shared.Next(30, 60);

            await cache.SetStringAsync(
                    key: key,
                    value: token.AccessToken,
                    new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(ttl) },
                    cancellationToken
                );
        }
    }
}
