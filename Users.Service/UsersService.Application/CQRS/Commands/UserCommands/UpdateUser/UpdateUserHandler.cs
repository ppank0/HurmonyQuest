﻿using AutoMapper;
using MediatR;
using UsersService.Application.DTOs;
using UsersService.Domain.Exceptions;
using UsersService.Domain.Interfaces;

namespace UsersService.Application.CQRS.Commands.UserCommands.UpdateUser
{
    public class UpdateUserHandler(IUserRepository repository, IMapper mapper) : IRequestHandler<UpdateUserCommand, UserDto>
    {
        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userToUpdate = await repository.GetByIdAsync(request.Id, cancellationToken);

            if (userToUpdate is null)
            {
                throw new NotFoundException(request.Id);
            }

            userToUpdate.UserPictureUrl = request.UserDto.UserPictureUrl;

            var updatedUser = await repository.UpdateAsync(userToUpdate, cancellationToken);

            return mapper.Map<UserDto>(updatedUser);
        }
    }
}
