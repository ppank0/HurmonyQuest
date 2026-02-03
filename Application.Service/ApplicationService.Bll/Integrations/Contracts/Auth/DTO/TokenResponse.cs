using System.Text.Json.Serialization;

namespace ApplicationService.BLL.Integrations.Contracts.Auth.DTO
{
    public record TokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; init; } = default!;

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; init; }

        [JsonPropertyName("scope")]
        public string Scope { get; init; } = default!;
    }
}
