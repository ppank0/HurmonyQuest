using ApplicationService.DAL.Context;
using ApplicationService.DAL.Entities;
using ApplicationService.DAL.Repositories.Interfaces;

namespace ApplicationService.DAL.UnitOfWork
{
    public class UnitOfWork(AppDbContext context,
        IBaseRepository<ApplicationEntity> appRepo,
        IBaseRepository<VideoEntity> videoRepo) : IUnitOfWork
    {
        private readonly AppDbContext _context = context;
        public IBaseRepository<ApplicationEntity> Applications => appRepo;
        public IBaseRepository<VideoEntity> Videos => videoRepo;

        public async Task SaveAsync(CancellationToken ct)
        {
            await _context.SaveChangesAsync(ct);
        }
    }
}
