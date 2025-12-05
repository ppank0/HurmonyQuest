using ApplicationService.BLL.Integrations.Contracts.Instruments.DTOs;

namespace ApplicationService.BLL.Integrations.Contracts.Instruments
{
    public interface IInstrumentHttpClient
    {
        Task<InstrumentResponse> GetByIdAsync(Guid id, CancellationToken ct);
        Task<List<InstrumentResponse>> GetAllAsync();
    }
}
