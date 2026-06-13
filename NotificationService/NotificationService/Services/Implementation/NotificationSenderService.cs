using Microsoft.AspNetCore.SignalR;
using NotificationService.Data.Enums;
using NotificationService.Data.Models;
using NotificationService.Services.Hubs;
using NotificationService.Services.Interfaces;

namespace NotificationService.Services.Implementation
{
    public class NotificationSenderService(IHubContext<NotificationHub> hubContext) : INotificationSenderService
    {
        private static void SetTargetType(NotificationModel notification, CancellationToken ct)
        {
            notification.TargetType = (notification.UserId, notification.TargetGroup) switch
            {
                (not null, null) => TargetType.User,
                (null, not null) => TargetType.Group,
                _ => throw new InvalidOperationException("Notification must have exactly one target (User or Group).")
            };
        }

        public async Task SendNotification(NotificationModel notification, CancellationToken ct)
        {
            SetTargetType(notification, ct);

            try
            {
                switch (notification.TargetType)
                {
                    case TargetType.User:
                        {
                            await hubContext.Clients.User(notification.UserId!)
                                .SendAsync("ReceiveMessage", notification.Id.ToString(), notification, ct);
                            break;
                        }
                    case TargetType.Group:
                        {
                            //await hubContext.Clients.Group(notification.TargetGroup!)
                            //   .SendAsync("ReceiveMessage", notification, ct);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
