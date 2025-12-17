namespace ApplicationService.DAL.Entities
{
    public class VideoEntity : BaseEntity
    {
        public string? VideoUrl { get; set; }
        public ApplicationEntity Application { get; set; } = null!;
    }
}
