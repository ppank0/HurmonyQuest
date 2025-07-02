using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersService.Application.DTOs
{
    public record CreateUserDto(string Email, string UserPictureUrl, string AuthId);
}
