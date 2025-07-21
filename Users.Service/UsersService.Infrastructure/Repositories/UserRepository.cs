using Microsoft.EntityFrameworkCore;
using UsersService.Domain.Exceptions;
using UsersService.Infrastructure.Context;
using UsersService.Domain.Entities;
using UsersService.Domain.Interfaces;

namespace UsersService.Infrastructure.Repositories
{
    public class UserRepository(UsersDBContext context) : IUserRepository
    {
        public async Task AddAsync(UserEntity user, CancellationToken cancellationToken)
        {
            var createdUser = context.Users.Add(user);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entityToDelete = await context.Users.FindAsync(id, cancellationToken);

            if (entityToDelete is null)
            {
                throw new NotFoundException(id);
            }

            context.Users.Remove(entityToDelete);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<UserEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await context.Users.ToListAsync(cancellationToken);
        }

        public async Task<UserEntity> GetByAuthIdAsync(string AuthId, CancellationToken cancellationToken)
        {
            if(AuthId is null)
            {
                throw new ArgumentNullException(nameof(AuthId));
            }

            var result = await context.Users.FirstAsync(x => x.AuthId == AuthId, cancellationToken);

            if (result is null)
            {
                throw new NotFoundException($"Entity with this AuthId: {AuthId} was not found");
            }

            return result;
        }

        public async Task<UserEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await context.Users.FindAsync(id, cancellationToken);

            if (user is null)
            {
                throw new NotFoundException(id);
            }

            return user;
        }

        public async Task<UserEntity> UpdateAsync(UserEntity user, CancellationToken cancellationToken)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync(cancellationToken);
            return user;
        }
    }
}
