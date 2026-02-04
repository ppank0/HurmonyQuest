using ApplicationService.DAL.Repositories.Interfaces;

<<<<<<< feature/9-create-integration-tests
namespace ApplicationService.DAL.Entities
{
    public class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
=======
namespace ApplicationService.DAL.Entities;
public class BaseEntity : IBaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
>>>>>>> main
}
