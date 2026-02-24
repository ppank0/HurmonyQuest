using MongoDB.Bson;
using NotificationService.Data.Enums;

namespace NotificationService.Data.Dtos
{
    public record NotificationDto(ObjectId Id, string Title, string Message,
                NotificationStatus Status, string? UserId, string? TargetGroup, bool IsRead);
}
