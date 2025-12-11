using ApplicationService.BLL.Models;
using ApplicationService.BLL.Models.Requests;

namespace ApplicationService.BLL.Interfaces
{
    public interface IApplicationService
    {
        Task<ApplicationModel> CreateAsync(CreateApplicationRequest request, CancellationToken ct);
        Task<List<ApplicationModel>> GetAllAsync(CancellationToken ct);
        Task<ApplicationModel> GetByIdAsync(Guid id, CancellationToken ct);
        Task<ApplicationModel> UpdateAsync(UpdateApplicationRequest request, CancellationToken ct);
        Task DeleteAsync(Guid id, CancellationToken ct);
    }
}
