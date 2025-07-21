using UsersService.Domain.Entities;

namespace UsersService.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(UserEntity user, CancellationToken cancellationToken);
        Task<UserEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<UserEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<UserEntity> GetByAuthIdAsync(string AuthId, CancellationToken cancellationToken);
        Task<UserEntity> UpdateAsync(UserEntity user, CancellationToken cancellationToken);
    }
}
