using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersService.Application.DTOs;

namespace UsersService.Application.CQRS.Queries.GetUsers
{
    public record GetUsersQuery() : IRequest<IEnumerable<UserDto>>
    {
    }
}
