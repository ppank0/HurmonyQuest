using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestService.BLL.Models;
public class JuryModel :ModelBase
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public DateOnly Birthday { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public Guid UserId { get; set; }
}
