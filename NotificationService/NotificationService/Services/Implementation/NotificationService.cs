using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using NotificationService.Const;
using NotificationService.Data.Entities;
using NotificationService.Data.Enums;
using NotificationService.Data.Models;
using NotificationService.Services.Interfaces;

namespace NotificationService.Services.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly IMongoCollection<NotificationEntity> _notifications;
        private readonly IMapper _mapper;
        private readonly INotificationSenderService _notificationSender;
        public NotificationService(IMongoDatabase mongoDatabase, IMapper mapper,
            INotificationSenderService notificationSender)
        {
            _notifications = mongoDatabase.GetCollection<NotificationEntity>(MongoConst.CollectionName);
            _mapper = mapper;
            _notificationSender = notificationSender;
        }
        public async Task CreateAsync(NotificationModel model, CancellationToken ct)
        {
            var entity = _mapper.Map<NotificationEntity>(model);
            await _notifications.InsertOneAsync(entity, cancellationToken: ct);
            await SendAsync(_mapper.Map<NotificationModel>(entity), ct);
        }
        public async Task SendAsync(NotificationModel model, CancellationToken ct)
        {
            await _notificationSender.SendNotification(model, ct);
            await UpdateStatus(model.Id.ToString(), NotificationStatus.Sent, ct);
        }

        public async Task<List<NotificationModel>> GetAll(CancellationToken ct)
        {
            var filter = Builders<NotificationEntity>.Filter.Empty;
            var entityList = await _notifications.Find(filter).ToListAsync(ct);

            return _mapper.Map<List<NotificationModel>>(entityList);
        }
        public async Task UpdateStatus(string id, NotificationStatus newStatus, CancellationToken ct)
        {
            var filter = Builders<NotificationEntity>.Filter.Eq(x => x.Id, ObjectId.Parse(id));
            var update = Builders<NotificationEntity>.Update.Set(x => x.Status, newStatus);

            await _notifications.UpdateOneAsync(filter, update, cancellationToken: ct);
        }

        public async Task DeleteAsync(string id, CancellationToken ct)
        {
            var filter = Builders<NotificationEntity>.Filter.Eq(x => x.Id, ObjectId.Parse(id));
            await _notifications.DeleteOneAsync(filter, cancellationToken: ct);
        }
    }
}
