using MongoDB.Bson;
using NotificationService.Data.Enums;

namespace NotificationService.Data.Models
{
    public class NotificationModel
    {
        public ObjectId Id { get; set; }
        public required string Title { get; set; }
        public required string Message { get; set; }
        public string? MessageId {  get; set; }

        public NotificationStatus Status { get; set; }

        public TargetType TargetType { get; set; }
        public string? UserId { get; set; }
        public string? TargetGroup { get; set; }
    }
}
