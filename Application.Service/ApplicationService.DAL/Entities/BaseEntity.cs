using ApplicationService.DAL.Repositories.Interfaces;

namespace ApplicationService.DAL.Entities
{
    public class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
