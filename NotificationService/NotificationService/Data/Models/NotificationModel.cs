using MongoDB.Bson;
using NotificationService.Data.Enums;

namespace NotificationService.Data.Models
{
    public class NotificationModel
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;

        public NotificationStatus Status { get; set; }

        public TargetType TargetType { get; set; }
        public string? UserId { get; set; }
        public string? TargetGroup { get; set; }
    }
}
