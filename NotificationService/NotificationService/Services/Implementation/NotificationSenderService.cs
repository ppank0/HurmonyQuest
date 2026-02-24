using NotificationService.Data.Enums;
using NotificationService.Data.Models;
using NotificationService.Services.Interfaces;

namespace NotificationService.Services.Implementation
{
    public class NotificationSenderService() : INotificationSenderService
    {
        private void FindoutTargetType(NotificationModel notification, CancellationToken ct)
        {
            if (notification.UserId is not null && notification.TargetGroup is null)
            {
                notification.TargetType = TargetType.User;
            }
            else if (notification.TargetGroup is not null && notification.UserId is null)
            {
                notification.TargetType = TargetType.Group;
            }
            else
            {
                throw new Exception("There cannot be just one type of target, or both at the same time.");
            }
        }

        public async Task SendNotification(NotificationModel notification, CancellationToken ct)
        {
            FindoutTargetType(notification, ct);

            switch (notification.TargetType)
            {
                case TargetType.User:
                    {
                        //await hubContext.Clients.User(notification.UserId!)
                        //    .SendAsync("ReceiveMessage", notification, ct);
                        break;
                    }
                case TargetType.Group:
                    {
                        //await hubContext.Clients.Group(notification.TargetGroup!)
                        //    .SendAsync("ReceiveMessage", notification, ct);
                        break;
                    }
            }
        }
    }
}
