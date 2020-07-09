using LiveShow.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LiveShow.Api.Controllers
{
    public class NotificationController : LiveShowApiControllerBase
    {
        private readonly INotificationService notificationService;

        public NotificationController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        [HttpGet("notifications/{id}")]
        public async Task<IActionResult> GetNotification(Guid id)
        {
            var notification = await notificationService.GetNotification(id);
            return Ok(notification);
        }
    }
}
