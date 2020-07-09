using LiveShowClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiveShowClient.Interfaces
{
    public interface IUserNotificationService
    {
        Task<IEnumerable<UserNotificationVM>> GetNotifications(Guid userId);
        Task<IEnumerable<UserNotificationVM>> GetUnreadNotifications(Guid userId);
        Task MarkNotificationAsRead(Guid id, Guid userId);
    }
}
