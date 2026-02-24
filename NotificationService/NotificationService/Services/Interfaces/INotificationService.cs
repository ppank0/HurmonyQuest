using MongoDB.Bson;
using NotificationService.Data.Models;

namespace NotificationService.Services.Interfaces
{
    public interface INotificationService
    {
        Task CreateAsync(NotificationModel model, CancellationToken ct);
        Task DeleteAsync(string id, CancellationToken ct);
        Task<List<NotificationModel>> GetAll(CancellationToken ct);
        Task<NotificationModel> UpdateAsync(string id, EditNotificationModel editNotification, CancellationToken ct);
    }
}
