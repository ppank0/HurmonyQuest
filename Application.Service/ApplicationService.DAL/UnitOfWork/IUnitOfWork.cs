using ApplicationService.DAL.Entities;
using ApplicationService.DAL.Repositories.Interfaces;

namespace ApplicationService.DAL.UnitOfWork
{
    public interface IUnitOfWork 
    {
        IBaseRepository<ApplicationEntity> Applications { get; }
        IBaseRepository<VideoEntity> Videos { get; }

        Task SaveAsync(CancellationToken ct);
    }
}
