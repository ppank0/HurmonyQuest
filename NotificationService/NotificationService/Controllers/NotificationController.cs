using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Data.Dtos;
using NotificationService.Data.Models;
using NotificationService.Services.Interfaces;

namespace NotificationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController(INotificationService notificationService, IMapper mapper) : ControllerBase
    {
        [HttpPost]
        public async Task Create(NotificationDto notification, CancellationToken ct)
        {
            if (notification is null)
            {
                throw new ArgumentNullException(nameof(notification));
            }
            await notificationService.CreateAsync(mapper.Map<NotificationModel>(notification), ct);
        }

        [HttpGet]
        public async Task<List<NotificationDto>> GetAll(CancellationToken ct)
        {
            var list = await notificationService.GetAll(ct);
            return mapper.Map<List<NotificationDto>>(list);
        }

        [HttpPatch("{id}")]
        public async Task<NotificationDto> Update(string id, EditNotificationDto notificationDto, CancellationToken ct)
        {
            if (notificationDto is null)
            {
                throw new Exception("Update can't be made");
            }
            var updatedNotification = await notificationService.UpdateAsync(id,
                                                mapper.Map<EditNotificationModel>(notificationDto), ct);
            return mapper.Map<NotificationDto>(updatedNotification);
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id, CancellationToken ct)
        {
            await notificationService.DeleteAsync(id, ct);
        }
    }
}
