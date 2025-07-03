using MediatR;
using UsersService.Application.DTOs;

namespace UsersService.Application.CQRS.Queries.GetUser
{
    public record GetUserQuery(Guid id) : IRequest<UserDto>
    {
    }
}
