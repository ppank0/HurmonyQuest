using ApplicationService.BLL.Exceptions;
using ApplicationService.BLL.Integrations.Contracts.Instruments;
using ApplicationService.BLL.Integrations.Contracts.Instruments.DTOs;

namespace Application.Service.HttpClients
{
    public class InstrumentHttpClient(HttpClient _httpClient) : IInstrumentHttpClient
    {
        public Task<List<InstrumentResponse>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<InstrumentResponse> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var res = await _httpClient.GetAsync($"/api/instruments/{id}", ct);
            res.EnsureSuccessStatusCode();
            var data = await res.Content.ReadFromJsonAsync<InstrumentResponse>(ct);
            if (data is null)
            {
                throw new InvalidResponseException("Empty response from instrument service");
            }
            return data;
        }
    }
}
