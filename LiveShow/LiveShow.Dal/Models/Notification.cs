using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiveShow.Dal.Models
{
    public class Notification
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime OriginalDateTime { get; set; }
        [MaxLength(100)]
        public string OriginalVenue { get; set; }
        
        public Guid ShowId { get; set; }
        public Show Show { get; set; }
        public ICollection<UserNotification> UserNotifications { get; set; }

        public Notification()
        {
            UserNotifications = new List<UserNotification>();
        }
    }
}
