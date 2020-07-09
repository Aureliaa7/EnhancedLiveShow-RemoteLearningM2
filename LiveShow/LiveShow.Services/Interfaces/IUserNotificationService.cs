using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiveShow.Services.Models.Notification;

namespace LiveShow.Services.Interfaces
{
    public interface IUserNotificationService
    {
        Task<IEnumerable<UserNotificationDto>> GetAllNotifications(Guid userId);
        Task<IEnumerable<UserNotificationDto>> GetUnreadNotifications(Guid userId);
        Task MarkNotificationAsRead(Guid notificationId, Guid userId);
    }
}
