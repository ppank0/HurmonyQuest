using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Data.Dtos;
using NotificationService.Data.Enums;
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
            await notificationService.CreateAsync(mapper.Map<NotificationModel>(notification), ct);
        }

        [HttpGet]
        public async Task<List<NotificationDto>> GetAll(CancellationToken ct)
        {
            var list = await notificationService.GetAll(ct);
            return mapper.Map<List<NotificationDto>>(list);
        }

        [HttpPatch("{id}")]
        public async Task UpdateStatus(string id, NotificationStatus newStatus, CancellationToken ct)
        {
            await notificationService.UpdateStatus(id, newStatus, ct); 
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id, CancellationToken ct)
        {
            await notificationService.DeleteAsync(id, ct);
        }
    }
}
