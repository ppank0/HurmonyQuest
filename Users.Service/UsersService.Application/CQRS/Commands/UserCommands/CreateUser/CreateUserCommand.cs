using MediatR;
using UsersService.Application.DTOs;


namespace UsersService.Application.CQRS.Commands.UserCommands.CreateUser
{
    public record CreateUserCommand(CreateUserDto UserDto) : IRequest<UserDto>
    {
    }
}
