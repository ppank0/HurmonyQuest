using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DAL.Repositories.Interfaces
{
    public interface IBaseEntity : IHasTimestamps
    {
        Guid Id { get; set; }
    }
}
