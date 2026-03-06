using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Data.Enums;
using NotificationService.Extensions;
using NotificationService.Services.Interfaces;
using RabbitMQ.Client;

namespace NotificationService.Services.Hubs
{
    [Authorize]
    public class NotificationHub(INotificationService notificationService, ConnectionMapping<string> _connections) : Hub
    {
        public override Task OnConnectedAsync()
        {
            string name = Context.UserIdentifier;
            _connections.Add(name, Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            string name = Context.User.Identity.Name;

            _connections.Remove(name, Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }

        public async Task ConfirmReceipt(string notificationId)
        {
            await notificationService.UpdateStatus(notificationId, NotificationStatus.Sent, Context.ConnectionAborted);
        }
    }
}
