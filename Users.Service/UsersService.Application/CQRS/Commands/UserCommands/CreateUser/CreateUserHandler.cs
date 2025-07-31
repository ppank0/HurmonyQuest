using AutoMapper;
using MediatR;
using UsersService.Application.DTOs;
using UsersService.Domain.Entities;
using UsersService.Domain.Exceptions;
using UsersService.Domain.Interfaces;

namespace UsersService.Application.CQRS.Commands.UserCommands.CreateUser
{
    public class CreateUserHandler(IUserRepository repository, IMapper mapper) : IRequestHandler<CreateUserCommand, UserDto>
    {

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existing = await repository.GetByAuthIdAsync(request.UserDto.AuthId, cancellationToken);

            if (existing is not null)
            {
                throw new ConflictException("User already exists");
            }

            var user = mapper.Map<UserEntity>(request.UserDto);

            await repository.AddAsync(user, cancellationToken);

            return mapper.Map<UserDto>(user);
        }
    }
}
