using ApplicationService.BLL.Integrations.Contracts.Auth.DTO;

namespace ApplicationService.BLL.Integrations.Contracts.Duende
{
    public interface IDuendeHttpClient
    {
        Task<TokenResponse> GetTokenAsync(string downstream, CancellationToken ct);
    }
}
