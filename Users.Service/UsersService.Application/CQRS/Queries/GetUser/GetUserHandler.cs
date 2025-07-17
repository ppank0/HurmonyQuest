using AutoMapper;
using MediatR;
using UsersService.Application.DTOs;
using UsersService.Domain.Exceptions;
using UsersService.Domain.Interfaces;

namespace UsersService.Application.CQRS.Queries.GetUser
{
    public class GetUserHandler(IUserRepository repository, IMapper mapper) : IRequestHandler<GetUserQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await repository.GetByIdAsync(request.id);

            if (user is null)
            {
                throw new NotFoundException(request.id);
            }

            return mapper.Map<UserDto>(user);
        }
    }
}
