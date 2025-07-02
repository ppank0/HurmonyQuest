using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersService.Domain.Exceptions
{
    public class NotFoundException (string message) : Exception (message)
    {
        public NotFoundException(Guid id) : this($"Entity with this id: {id} was not found") { }
    }
}
