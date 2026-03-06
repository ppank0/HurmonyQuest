using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using NotificationService.Const;
using NotificationService.Extensions;
using NotificationService.Extensions.BackgroundWorkers;
using NotificationService.Extensions.Providers;
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

            services.AddSignalR();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://localhost:5001";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "https://localhost:5001", 
                    ValidateAudience = false,
                    ValidateLifetime = true
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/notifications")))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine(context.Exception.Message);
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization();

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
            services.AddSingleton<IUserIdProvider, NameUserIdProvider>();
            services.AddSingleton<ConnectionMapping<string>>();

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
