using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersService.Application.DTOs;
using UsersService.Domain.Interfaces;

namespace UsersService.Application.CQRS.Queries.GetUsers
{
    public class GetUsersHandler(IUserRepository repository, IMapper mapper) : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
    {
        public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await repository.GetAllAsync(cancellationToken);

            return mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
}
