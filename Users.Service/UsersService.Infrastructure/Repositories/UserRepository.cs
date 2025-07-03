using Microsoft.EntityFrameworkCore;
using UsersService.Domain.Exceptions;
using UsersService.Infrastructure.Context;
using UsersService.Domain.Entities;
using UsersService.Domain.Interfaces;

namespace UsersService.Infrastructure.Repositories
{
    public class UserRepository(UsersDBContext context) : IUserRepository
    {
        public async Task AddAsync(UserEntity user)
        {
            var createdUser = context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entityToDelete = await context.Users.FindAsync(id);

            if (entityToDelete is null)
            {
                throw new NotFoundException(id);
            }

            context.Users.Remove(entityToDelete);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<UserEntity> GetByAuthIdAsync(string AuthId)
        {
            if(AuthId is null)
            {
                throw new ArgumentNullException(nameof(AuthId));
            }

            var result = await context.Users.FirstOrDefaultAsync(x => x.AuthId == AuthId);

            if (result is null)
            {
                throw new NotFoundException($"Entity with this AuthId: {AuthId} was not found");
            }

            return result;
        }

        public async Task<UserEntity?> GetByIdAsync(Guid id)
        {
            var user = await context.Users.FindAsync(id);

            if (user is null)
            {
                throw new NotFoundException(id);
            }

            return user;
        }

        public async Task<UserEntity> UpdateAsync(UserEntity user)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return user;
        }
    }
}
