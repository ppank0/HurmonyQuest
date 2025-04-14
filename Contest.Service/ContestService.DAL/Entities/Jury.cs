using System.ComponentModel.DataAnnotations;

namespace ContestService.DAL.Entities
{
    internal class Jury : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public DateOnly Birthday { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        [Required]
        public Guid UserId { get; set; }
    }
}
