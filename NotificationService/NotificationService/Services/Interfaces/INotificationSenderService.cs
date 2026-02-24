using NotificationService.Data.Models;

namespace NotificationService.Services.Interfaces
{
    public interface INotificationSenderService
    {
        Task SendNotification(NotificationModel notification, CancellationToken ct);
    }
}
