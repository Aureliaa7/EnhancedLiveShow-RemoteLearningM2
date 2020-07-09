using LiveShow.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LiveShow.Api.Controllers
{
    public class UserNotificationController : LiveShowApiControllerBase
    {
        private readonly IUserNotificationService notificationService;

        public UserNotificationController(IUserNotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        [HttpGet("notifications/{id}")]
        public async Task<IActionResult> GetNotifications(Guid id)
        {
            var userNotifications = await notificationService.GetAllNotifications(id); 
            return Ok(userNotifications);
        }

        [HttpGet("unreadnotifications/{id}")]
        public async Task<IActionResult> GetUnreadNotifications(Guid id)
        {
            var unreadNotifications = await notificationService.GetUnreadNotifications(id);
            return Ok(unreadNotifications);
        }

        [HttpPatch("{id}/{userId}")]
        public async Task<IActionResult> MarkNotificationAsRead(Guid id, Guid userId)
        {
            await notificationService.MarkNotificationAsRead(id, userId);
            return Ok();
        }
    }
}