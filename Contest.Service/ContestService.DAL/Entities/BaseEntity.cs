using ContestService.DAL.Repositories.Interfaces;

namespace ContestService.DAL.Entities
{
    internal class BaseEntity : IHasTimestamps
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
