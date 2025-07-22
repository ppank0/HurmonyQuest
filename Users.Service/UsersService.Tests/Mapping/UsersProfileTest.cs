using AutoMapper;
using UsersService.Application.DTOs;
using UsersService.Domain.Entities;

namespace UsersService.Tests.Mapping
{
    internal class UsersProfileTest : Profile
    {
        public UsersProfileTest()
        {
            CreateMap<UserEntity, UserDto>();
            CreateMap<CreateUserDto, UserEntity>();
        }
    }
}
