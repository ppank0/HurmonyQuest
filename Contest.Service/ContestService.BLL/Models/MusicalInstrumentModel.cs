using ContestService.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestService.BLL.Models;
public class MusicalInstrumentModel : ModelBase
{
    public required string Name { get; set; }
    public Guid NominationId { get; set; }
    public Nomination Nomination { get; set; }
}
