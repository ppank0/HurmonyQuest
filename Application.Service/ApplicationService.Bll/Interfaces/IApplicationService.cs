using ApplicationService.BLL.CQRS.Command.ApplicationCommands.Create;
using ApplicationService.BLL.CQRS.Command.ApplicationCommands.Delete;
using ApplicationService.BLL.CQRS.Command.ApplicationCommands.Update;
using ApplicationService.BLL.CQRS.DTOs;

namespace ApplicationService.BLL.Interfaces
{
    public interface IApplicationService
    {
        Task<ApplicationDto> CreateAsync(CreateApplicationCommand request, CancellationToken ct);
        Task<List<ApplicationDto>> GetAllAsync(CancellationToken ct);
        Task<ApplicationDto> GetByIdAsync(Guid id, CancellationToken ct);
        Task<ApplicationDto> UpdateAsync(UpdateApplicationCommand request, CancellationToken ct);
        Task DeleteAsync(DeleteApplicationCommand request, CancellationToken ct);
    }
}
