using ApplicationService.BLL.Integrations.Contracts.Auth.DTO;

namespace ApplicationService.BLL.Repositories.Interfaces
{
    public interface ITokenCache
    {
        Task<string> GetTokenAsync(string key, CancellationToken cancellationToken);
        Task SetTokenAsync(string key, TokenResponse token, CancellationToken cancellationToken);
    }
}
