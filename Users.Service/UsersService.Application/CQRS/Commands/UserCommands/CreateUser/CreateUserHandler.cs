using MediatR;
using UsersService.Domain.Entities;
using UsersService.Domain.Interfaces;
using UsersService.Application.DTOs;
using UsersService.Domain.Exceptions;

namespace UsersService.Application.CQRS.Commands.UserCommands.CreateUser
{
    public class CreateUserHandler(IUserRepository repository) : IRequestHandler<CreateUserCommand, UserDto>
    {

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existing = await repository.GetByAuthIdAsync(request.UserDto.AuthId);

            if(existing is not null)
            {
                throw new BadRequestException("User already exists");
            }

            var user = new UserEntity
            {
                Email = request.UserDto.Email,
                UserPictureUrl = request.UserDto.UserPictureUrl,
                AuthId = request.UserDto.AuthId,
            }; 

            await repository.AddAsync(user);

            return new UserDto
            (
                user.Id,
                user.Email,
                user.UserPictureUrl,
                user.AuthId
            );
        }
    }
}
