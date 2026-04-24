using NotificationService.Data.Enums;
using NotificationService.Data.Models;
using NotificationService.Services.Interfaces;

namespace NotificationService.Services.Implementation
{
    public class NotificationSenderService() : INotificationSenderService
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

        public async Task<bool> SendNotification(NotificationModel notification, CancellationToken ct)
        {
            SetTargetType(notification, ct);

            try
            {
                switch (notification.TargetType)
                {
                    case TargetType.User:
                        {
                            //var isReceived = await hubContext.Clients.User(notification.UserId!)
                            //.InvokeAsync<bool>("ReceiveMessage", notification, ct);
                            var isReceived = true;
                            return isReceived;
                        }
                    case TargetType.Group:
                        {
                            //await hubContext.Clients.Group(notification.TargetGroup!)
                            //    .SendAsync("ReceiveMessage", notification, ct);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return false;
        }
    }
}
