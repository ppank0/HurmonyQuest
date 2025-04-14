using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContestService.DAL.Entities
{
    internal class Participant : BaseEntity
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Surname { get; set; } = null!;
        [Required]
        public DateOnly Birthday { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        [Required]
        [ForeignKey("MusicalInstrument")]
        public Guid MusicalInstrumentId { get; set; }
        public MusicalInstrument MusicalInstrument { get; set; }
        [Required]
        public Guid UserId { get; set; }


    }
}
