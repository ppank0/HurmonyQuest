using MediatR;
using UsersService.Application.DTOs;

namespace UsersService.Application.CQRS.Commands.UserCommands.UpdateUser
{
    public record UpdateUserCommand(Guid Id, UpdateUserDto UserDto) : IRequest<UserDto>;
}
