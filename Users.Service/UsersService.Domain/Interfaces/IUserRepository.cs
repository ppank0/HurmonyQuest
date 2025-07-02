using UsersService.Domain.Entities;

namespace UsersService.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(UserEntity user);
        Task<UserEntity?> GetByIdAsync(Guid id);
        Task<IEnumerable<UserEntity>> GetAllAsync();
        Task DeleteAsync(Guid id);
        Task<UserEntity> GetByAuthIdAsync(string AuthId);
        Task<UserEntity> UpdateAsync(UserEntity user);
    }
}
