using System;

namespace LiveShow.Dal.Models
{
    public class UserNotification
    {
        public Guid UserId { get; set; }
        public Guid NotificationId { get; set; }
        public bool IsRead { get; set; }

        public User User { get; set; }
        public Notification Notification { get; set; }
    }
}
