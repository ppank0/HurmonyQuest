using ApplicationService.BLL.Integrations.Contracts.Auth.DTO;
using ApplicationService.BLL.Integrations.Contracts.Duende;

namespace Application.Service.HttpClients
{
    public class DuendeHttpClient(HttpClient client, IConfiguration configuration) : IDuendeHttpClient
    {
        public async Task<TokenResponse> GetTokenAsync(string downstream, CancellationToken ct)
        {
            var form = new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = configuration["DuendeIdentityServer:ClientId"]!,
                ["client_secret"] = configuration["DuendeIdentityServer:ClientSecret"]!,
                ["scope"] = configuration[$"DuendeIdentityServer:Downstream:{downstream}:Scopes"]!
            };
            using var content = new FormUrlEncodedContent(form);

            var res = await client.PostAsync($"/connect/token", content);
            res.EnsureSuccessStatusCode();

            var token = await res.Content.ReadFromJsonAsync<TokenResponse>();
            return token!;
        }
    }
}
