using System.ComponentModel.DataAnnotations;

namespace ContestService.DAL.Entities
{
    internal class Nomination : BaseEntity
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}
