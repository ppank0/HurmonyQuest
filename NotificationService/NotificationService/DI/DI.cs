using MassTransit;
using MongoDB.Driver;
using NotificationService.Const;
using NotificationService.Mapper;
using NotificationService.Services.Consumers;
using NotificationService.Services.Implementation;
using NotificationService.Services.Interfaces;

namespace NotificationService.DI
{
    public static class DI
    {
        public static IServiceCollection AddDependences(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoConnectionString = configuration.GetConnectionString("MongoDbConnection");
            var rabbitmqUsername = configuration.GetRequiredSection("RabbitMq:UserName").Value;
            var rabbitmqPassword = configuration.GetRequiredSection("RabbitMq:Password").Value;

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

            services.AddMassTransit(x =>
            {
                x.AddConsumers(typeof(ApplicationConsumer).Assembly);

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", host =>
                    {
                        host.Username(rabbitmqUsername!);
                        host.Password(rabbitmqPassword!);
                    });

                    cfg.ReceiveEndpoint("ApplicationQueue", e =>
                    {
                        e.ConfigureConsumer<ApplicationConsumer>(context);
                    });
                });
            });

            return services;
        }
    }
}
