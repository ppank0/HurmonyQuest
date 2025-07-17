using MediatR;

namespace UsersService.Application.CQRS.Commands.UserCommands.DeleteUser
{
    public record DeleteUserCommand(Guid Id) : IRequest;
}
