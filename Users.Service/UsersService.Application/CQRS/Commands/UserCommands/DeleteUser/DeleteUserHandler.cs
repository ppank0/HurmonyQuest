using MediatR;
using UsersService.Domain.Interfaces;
using UsersService.Domain.Exceptions;

namespace UsersService.Application.CQRS.Commands.UserCommands.DeleteUser
{
    internal class DeleteUserHandler(IUserRepository repository) : IRequestHandler<DeleteUserCommand>
    {
        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userToDelete = await repository.GetByIdAsync(request.Id, cancellationToken);

            if (userToDelete is null)
            {
                throw new NotFoundException(request.Id);
            }

            await repository.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
