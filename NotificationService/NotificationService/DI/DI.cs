using MongoDB.Driver;
using NotificationService.Const;
using NotificationService.Mapper;
using NotificationService.Services.Implementation;
using NotificationService.Services.Interfaces;

namespace NotificationService.DI
{
    public static class DI
    {
        public static IServiceCollection AddDependences(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoConnectionString = configuration.GetConnectionString("MongoDbConnection");
            services.AddScoped<IMongoDatabase>(sp =>
            {
                var client = new MongoClient(mongoConnectionString);
                var db = client.GetDatabase(MongoConst.DatabaseName);
                return db;
            });

            services.AddAutoMapper(sp =>
            {
                sp.AddProfile(new MapperProfile());
            });

            services.AddScoped<INotificationService, Services.Implementation.NotificationService>();
            services.AddScoped<INotificationSenderService, NotificationSenderService>();

            return services;
        }
    }
}
