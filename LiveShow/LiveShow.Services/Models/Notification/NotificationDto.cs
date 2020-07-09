using System;

namespace LiveShow.Services.Models.Notification
{
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public NotificationTypeDto Type { get; set; }
        public DateTime OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }

        public Guid ShowId { get; set; }
    }
}
