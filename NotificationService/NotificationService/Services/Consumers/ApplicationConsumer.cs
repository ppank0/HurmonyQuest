using MassTransit;
using NotificationService.Data.Models;
using NotificationService.Extensions.Exceptions;
using NotificationService.Services.Interfaces;
using SharedModels.Contracts.Applications;
using SharedModels.Contracts.Applications.Data;

namespace NotificationService.Services.Consumers
{
    public class ApplicationConsumer(INotificationService notificationService) : IConsumer<IApplicationEvent>
    {
        public async Task Consume(ConsumeContext<IApplicationEvent> context)
        {
            await (context.Message.ActionToApplication switch
            {
                ActionType.Create => ConsumeApplicationCreated(context),
                ActionType.Update => ConsumeApplicationStatusUpdated(context),
                ActionType.Delete => ConsumeApplicationDeleted(context),
                _ => throw new NoActionTypeException($"Unsupported action type: {context.Message.ActionToApplication}." +
                " No handler is implemented for this event.\"")
            });
        }

        public async Task ConsumeApplicationCreated(ConsumeContext<IApplicationEvent> context)
        {
            var notificationModel = new NotificationModel
            {
                Title = $"Your Application was Created!",
                Message = $"{context.Message.applicationData.ParticipantName}, your application was created." +
                $"\nCurrent status: {context.Message.applicationData.ApplicationStatus}." +
                $" We'll notify you of any updates.",
                UserId = context.Message.UserId,
                TargetGroup = context.Message.TargetGroup,
            };

            await notificationService.CreateAsync(notificationModel, context.CancellationToken);
        }

        public async Task ConsumeApplicationStatusUpdated(ConsumeContext<IApplicationEvent> context)
        {
            var notificationModel = new NotificationModel
            {
                Title = $"Application Status was Updated!",
                Message = $"{context.Message.applicationData.ParticipantName}, your application status was updated." +
                $"\nCurrent status: {context.Message.applicationData.ApplicationStatus}.",
                UserId = context.Message.UserId,
                TargetGroup = context.Message.TargetGroup,
            };

            await notificationService.CreateAsync(notificationModel, context.CancellationToken);
        }
        public async Task ConsumeApplicationDeleted(ConsumeContext<IApplicationEvent> context)
        {
            var notificationModel = new NotificationModel
            {
                Title = $"Application was deleted",
                Message = $"Your application was successfully deleted.",
                UserId = context.Message.UserId,
                TargetGroup = context.Message.TargetGroup,
            };

            await notificationService.CreateAsync(notificationModel, context.CancellationToken);
        }
    }
}
