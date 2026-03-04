using NotificationService.Data.Models;

namespace NotificationService.Services.Interfaces
{
    public interface INotificationSenderService
    {
        Task<bool> SendNotification(NotificationModel notification, CancellationToken ct);
    }
}
