using AutoMapper;
using MongoDB.Driver;
using NotificationService.Const;
using NotificationService.Data.Entities;
using NotificationService.Data.Enums;
using NotificationService.Data.Models;
using NotificationService.Services.Interfaces;

namespace NotificationService.Extensions.BackgroundWorkers
{
    public class NotificationOutboxWorker(IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested == false)
            {
                try
                {
                    await ResendDataToSignalR(stoppingToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: ", ex);
                }

                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
            }
        }

        private async Task ResendDataToSignalR(CancellationToken stoppingToken)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
                var collection = db.GetCollection<NotificationEntity>(MongoConst.CollectionName);
                var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
                var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                var cutoff = DateTime.UtcNow.AddMinutes(-1);
                var unsentData = await collection.Find(n => n.Status == NotificationStatus.Created &&
                    n.CreatedAt < cutoff).ToListAsync(stoppingToken);

                if(unsentData.Count == 0)
                {
                    return;
                }

                foreach (var notification in unsentData)
                {
                    if(await CheckAndUpdateRetryInfo(notification, collection, stoppingToken))
                    {
                        await notificationService.SendAsync(mapper.Map<NotificationModel>(notification), stoppingToken);
                    }
                }
            }
        }

        private async Task<bool> CheckAndUpdateRetryInfo(NotificationEntity entity,
            IMongoCollection<NotificationEntity> collection, CancellationToken ct)
        {
            if (entity.RetryCount < 5)
            {
                entity.RetryCount += 1;
                entity.LastRetryAt = DateTime.UtcNow;
                var update = Builders<NotificationEntity>.Update
                    .Set(n => n.RetryCount, entity.RetryCount)
                    .Set(n => n.LastRetryAt, entity.LastRetryAt);

                await collection.UpdateOneAsync(n => n.Id == entity.Id, update, cancellationToken: ct);
                return true;
            }

            return false;
        }
    }
}
