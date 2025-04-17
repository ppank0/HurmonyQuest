using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestService.BLL.Exceptions;
public class NotFoundException : Exception
{
    public NotFoundException(string message):base (message) { }
    public NotFoundException(Guid id) : base($"Сущность с таким id: {id} не найдена") { }
};
