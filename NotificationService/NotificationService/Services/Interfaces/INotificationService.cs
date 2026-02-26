using MongoDB.Bson;
using NotificationService.Data.Enums;
using NotificationService.Data.Models;

namespace NotificationService.Services.Interfaces
{
    public interface INotificationService
    {
        Task CreateAsync(NotificationModel model, CancellationToken ct);
        Task DeleteAsync(string id, CancellationToken ct);
        Task<List<NotificationModel>> GetAll(CancellationToken ct);
        Task UpdateStatus(string id, NotificationStatus newStatus, CancellationToken ct);
    }
}
