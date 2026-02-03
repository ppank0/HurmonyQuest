using Application.Service.HttpClients.Endpoints;
using ApplicationService.BLL.Exceptions;
using ApplicationService.BLL.Integrations.Contracts.Participant;
using ApplicationService.BLL.Integrations.Contracts.Participants.DTOs;

namespace Application.Service.HttpClients
{
    public class ParticipantHttpClient(HttpClient _httpClient) : IParticipantHttpClient
    {
        public async Task<ParticipantResponse> CreateAsync(ParticipantCreateRequest req, CancellationToken ct)
        {
            var res = await _httpClient.PostAsJsonAsync(ParticipantEndpoints.Base, req, ct);
            res.EnsureSuccessStatusCode();
            var data = await res.Content.ReadFromJsonAsync<ParticipantResponse>(ct);
            
            return EnsureResponseNotNull(data);
        }

        public async Task<ParticipantResponse> GetAsync(Guid id, CancellationToken ct)
        {
            var res = await _httpClient.GetAsync(ParticipantEndpoints.ById(id), ct);
            res.EnsureSuccessStatusCode();
            var data = await res.Content.ReadFromJsonAsync<ParticipantResponse>(ct);
            
            return EnsureResponseNotNull(data);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var res = await _httpClient.DeleteAsync(ParticipantEndpoints.ById(id), ct);
            res.EnsureSuccessStatusCode();
        }

        private static ParticipantResponse EnsureResponseNotNull(ParticipantResponse? data)
        {
            if (data is null)
            {
                throw new InvalidResponseException("Empty response from participants service");
            }

            return data;
        }
    }
}
