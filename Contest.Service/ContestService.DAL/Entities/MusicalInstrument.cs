using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContestService.DAL.Entities
{
    internal class MusicalInstrument : BaseEntity
    {
        [Required]
        public string Name { get; set; } = null!;

        [ForeignKey("Nomination")]
        [Required]
        public Guid NominationId { get; set; }
        public Nomination Nomination { get; set; }
    }
}
