using System.ComponentModel.DataAnnotations;

namespace ContestService.DAL.Entities
{
    internal class Stage : BaseEntity
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        [Required]
        public DateTime EndDate { get; set; } = DateTime.UtcNow.AddDays(1);
    }
}
