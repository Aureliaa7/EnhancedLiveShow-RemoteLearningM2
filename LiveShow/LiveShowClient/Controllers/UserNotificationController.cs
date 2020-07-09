using LiveShowClient.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LiveShowClient.Controllers
{
    [Authorize]
    public class UserNotificationController : Controller
    {
        private readonly IUserNotificationService userNotificationService;
        private readonly IGettingCurrentUser currentUser;
        private readonly IHttpContextAccessor httpContext;

        public UserNotificationController(IUserNotificationService userNotificationService, IGettingCurrentUser currentUser, IHttpContextAccessor httpContext)
        {
            this.userNotificationService = userNotificationService;
            this.currentUser = currentUser;
            this.httpContext = httpContext;
        }

        [HttpGet]
        public async Task<IActionResult> UnreadNotifications()
        {
            var unreadNotifications = await userNotificationService.GetUnreadNotifications(currentUser.GetCurrentUserId(httpContext));
            return Ok(new { Notifications = unreadNotifications, Count = unreadNotifications.Count() });
        }

        public IActionResult MarkAsRead(string notificationId)
        {
            userNotificationService.MarkNotificationAsRead(new Guid(notificationId), currentUser.GetCurrentUserId(httpContext)).Wait();
            return Ok();
        }
    }
}
