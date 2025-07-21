﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersService.Application.DTOs
{
    public record UserDto(Guid Id, string Email, string UserPictureUrl, string AuthId);
}
