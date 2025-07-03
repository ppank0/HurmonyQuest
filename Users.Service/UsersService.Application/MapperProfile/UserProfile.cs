using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
