using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using UsersService.Application.CQRS.Commands.UserCommands.CreateUser;
using UsersService.Application.CQRS.Commands.UserCommands.DeleteUser;
using UsersService.Application.CQRS.Commands.UserCommands.UpdateUser;
using UsersService.Application.CQRS.Queries.GetUser;
using UsersService.Application.CQRS.Queries.GetUsers;
using UsersService.Application.DTOs;
using UsersService.Domain.Entities;
using UsersService.Domain.Exceptions;
using UsersService.Domain.Interfaces;
using UsersService.Tests.Customization;
using UsersService.Tests.Mapping;

namespace UsersService.Tests.HandlersTests
{
    public class UsersHandlerTests
    {
        private readonly IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<UsersProfileTest>()).CreateMapper();

        [Theory]
        [AutoDomainData]
        public async Task CreateUserHandler_UserDoesNotExist_ReturnsCreatedUser(
        [Frozen] IUserRepository userRepository,
        CreateUserCommand command,
        CreateUserHandler handler)
        {
            // Arrange
            userRepository.GetByAuthIdAsync(command.UserDto.AuthId, CancellationToken.None)
                .Returns((UserEntity?)null);

            userRepository.AddAsync(Arg.Any<UserEntity>(), CancellationToken.None)
                .Returns(Task.CompletedTask);

            var userEntity = mapper.Map<UserEntity>(command.UserDto);
            userEntity.Id = new Guid();
            var userDto = mapper.Map<UserDto>(userEntity);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(userDto);
            await userRepository.Received(1).AddAsync(Arg.Any<UserEntity>(), CancellationToken.None);
        }

        [Theory]
        [AutoDomainData]
        public async Task CreateUserHandler_UserAlreadyExists_ThrowsConflictException(
            [Frozen] IUserRepository userRepository,
            CreateUserCommand command,
            UserEntity userEntity,
            CreateUserHandler handler)
        {
            //Arrange
            userRepository.GetByAuthIdAsync(command.UserDto.AuthId, CancellationToken.None).Returns(userEntity);

            //Act
            var result = async () => await handler.Handle(command, CancellationToken.None);

            //Assert
            await Assert.ThrowsAsync<ConflictException>(result);
        }

        [Theory]
        [AutoDomainData]
        public async Task DeleteUserHandler_UserExists_ReturnNoContent(
            [Frozen] IUserRepository userRepository,
            Guid id,
            UserEntity userToDelete,
            DeleteUserHandler handler)
        {
            //Arrange
            userRepository.GetByIdAsync(id, CancellationToken.None).Returns(userToDelete);
            userRepository.DeleteAsync(id, CancellationToken.None).Returns(Task.CompletedTask);

            //Act
            var result = async () => await handler.Handle(new DeleteUserCommand(id), CancellationToken.None);

            //Assert
            await result.Should().NotThrowAsync();
        }

        [Theory]
        [AutoDomainData]
        public async Task DeleteUserHandler_UserNotFound_ThrowNotFound(
            [Frozen] IUserRepository userRepository,
            Guid id,
            DeleteUserHandler handler)
        {
            //Arrange
            userRepository.GetByIdAsync(id, CancellationToken.None).Returns((UserEntity)null);

            //Act
            var result = async () => await handler.Handle(new DeleteUserCommand(id), CancellationToken.None);

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(result);
        }

        [Theory]
        [AutoDomainData]
        public async Task UpdateUserHandler_UserExists_UpdateSuccessfully(
            [Frozen] IUserRepository userRepository,
            UpdateUserCommand command,
            UserEntity userToUpdate,
            UpdateUserHandler handler)
        {
            //Arrange
            userRepository.GetByIdAsync(command.Id, CancellationToken.None).Returns(userToUpdate);
            userToUpdate.UserPictureUrl = command.UserDto.UserPictureUrl;

            userRepository.UpdateAsync(userToUpdate, CancellationToken.None).Returns(userToUpdate);
            UserDto userDto = mapper.Map<UserDto>(userToUpdate);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().BeEquivalentTo(userDto);
        }

        [Theory]
        [AutoDomainData]
        public async Task UpdateUserHandler_UserNotFound_ThrowNotFound(
            [Frozen] IUserRepository userRepository,
            UpdateUserCommand command,
            UpdateUserHandler handler)
        {
            //Arrange
            userRepository.GetByIdAsync(command.Id, CancellationToken.None).Returns((UserEntity)null);

            //Act
            var result = async () => await handler.Handle(command, CancellationToken.None);

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(result);
        }

        [Theory]
        [AutoDomainData]
        public async Task GetUsersHandler_Valid_ReturnUsersList(
            [Frozen] IUserRepository userRepository,
            List<UserEntity> users,
            GetUsersHandler handler)
        {
            //Arrange
            userRepository.GetAllAsync(CancellationToken.None).Returns(users);
            var usersDto = mapper.Map<IEnumerable<UserDto>>(users);
            //Act
            var result = await handler.Handle(new GetUsersQuery(), CancellationToken.None);

            //Assert
            result.Should().BeEquivalentTo(usersDto);
        }

        [Theory]
        [AutoDomainData]
        public async Task Handle_UserExists_ReturnsUserDto(
            [Frozen] IUserRepository userRepository,
            [Frozen] IMapper mapper,
            Guid userId,
            UserEntity existingUser,
            GetUserHandler handler)
        {
            // Arrange
            existingUser.Id = userId;
            userRepository.GetByIdAsync(userId, CancellationToken.None).Returns(existingUser);
            var userDto = mapper.Map<UserDto>(existingUser);

            // Act
            var result = await handler.Handle(new GetUserQuery(userId), CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(userDto);
            await userRepository.Received(1).GetByIdAsync(userId, CancellationToken.None);
        }

        [Theory]
        [AutoDomainData]
        public async Task Handle_UserNotFound_ThrowsNotFoundException(
            [Frozen] IUserRepository userRepository,
            Guid userId,
            GetUserHandler handler)
        {
            // Arrange
            userRepository.GetByIdAsync(userId, CancellationToken.None).Returns((UserEntity?)null);

            // Act
            Func<Task> act = async () => await handler.Handle(new GetUserQuery(userId), CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}
