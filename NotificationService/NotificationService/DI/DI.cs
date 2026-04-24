using MassTransit;
using MongoDB.Driver;
using NotificationService.Const;
using NotificationService.Extensions.BackgroundWorkers;
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

            var mongoClient = new MongoClient(mongoConnectionString);
            services.AddSingleton<IMongoClient>(mongoClient);
            services.AddSingleton<IMongoDatabase>(sp =>
                sp.GetRequiredService<IMongoClient>().GetDatabase(MongoConst.DatabaseName));

            services.AddAutoMapper(sp =>
            {
                sp.AddProfile(new MapperProfile());
            });

            services.AddScoped<INotificationService, Services.Implementation.NotificationService>();
            services.AddScoped<INotificationSenderService, NotificationSenderService>();
            services.AddHostedService<NotificationOutboxWorker>();

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
                        e.UseMessageRetry(r => r.Exponential(3, TimeSpan.FromSeconds(3),
                            TimeSpan.FromSeconds(15), TimeSpan.FromSeconds(1)));
                        e.ConfigureConsumer<ApplicationConsumer>(context);
                    });
                });
            });

            return services;
        }
    }
}
