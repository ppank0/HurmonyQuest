using AutoMapper;
using UsersService.Application.DTOs;
using UsersService.Domain.Entities;

namespace UsersService.Application.MapperProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, UserDto>().ReverseMap();
        }
    }
}
