using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersService.Domain.Exceptions
{
    public class ConflictException(string message) : Exception(message);
}
