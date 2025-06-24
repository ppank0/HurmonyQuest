using UserService.DataAccess.Entities;

namespace UserService.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(UserEntity user);
        Task<UserEntity?> GetByIdAsync(Guid id);
        Task<IEnumerable<UserEntity>> GetAllAsync();
        Task DeleteAsync(Guid id);
    }
}
