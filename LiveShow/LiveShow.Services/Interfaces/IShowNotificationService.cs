using LiveShow.Services.Models.Notification;
using LiveShow.Services.Models.Show;
using System.Threading.Tasks;

namespace LiveShow.Services.Interfaces
{
    public interface IShowNotificationService
    {
        Task NotifyFollowers(ShowDto show, NotificationTypeDto notificationType);
        Task NotifyCreatedShow(ShowDto show);
    }
}
