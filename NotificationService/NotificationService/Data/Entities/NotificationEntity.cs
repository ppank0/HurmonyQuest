using MongoDB.Bson;
using NotificationService.Data.Enums;

namespace NotificationService.Data.Entities
{
    public class NotificationEntity
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;

        public bool IsRead { get; set; } = false;
        public NotificationStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? UserId { get; set; }
        public string? TargetGroup { get; set; }
    }
}
