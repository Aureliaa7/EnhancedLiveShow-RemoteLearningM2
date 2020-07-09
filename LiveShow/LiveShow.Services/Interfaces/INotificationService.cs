using LiveShow.Services.Models.Notification;
using System;
using System.Threading.Tasks;

namespace LiveShow.Services.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationDto> GetNotification(Guid id);
    }
}
