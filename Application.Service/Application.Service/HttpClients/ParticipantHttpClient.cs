using ApplicationService.BLL.Exceptions;
using ApplicationService.BLL.Integrations.Contracts.Participant;
using ApplicationService.BLL.Integrations.Contracts.Participants.DTOs;

namespace Application.Service.HttpClients
{
    public class ParticipantHttpClient(HttpClient httpClient) : IParticipantHttpClient
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<ParticipantResponse> CreateAsync(ParticipantCreateRequest req, CancellationToken ct)
        {
            var res = await _httpClient.PostAsJsonAsync("api/participants", req, ct);
            res.EnsureSuccessStatusCode();
            var data = await res.Content.ReadFromJsonAsync<ParticipantResponse>(ct);
            if (data is null)
            {
                throw new InvalidOperationException("Empty response from participants service");
            }
            
            return data;
        }

        public async Task<ParticipantResponse> GetAsync(Guid id, CancellationToken ct)
        {
            var res = await _httpClient.GetAsync($"api/participants/{id}", ct);
            res.EnsureSuccessStatusCode();
            var data = await res.Content.ReadFromJsonAsync<ParticipantResponse>(ct);
            if (data is null)
            {
                throw new InvalidResponseException("Empty response from participants service");
            }
            return data;
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var res = await _httpClient.DeleteAsync($"api/participants/{id}", ct);
            res.EnsureSuccessStatusCode();
        }
    }
}
