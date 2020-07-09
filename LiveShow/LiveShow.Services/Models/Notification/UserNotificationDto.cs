using LiveShow.Services.Models.User;
using System;
using System.ComponentModel.DataAnnotations;

namespace LiveShow.Services.Models.Notification
{
    public class UserNotificationDto
    {
        public Guid UserId { get; set; }
        public Guid NotificationId { get; set; }
        [Display(Name="Is read")]
        public bool IsRead { get; set; }

        public UserRegisterDto User { get; set; }
        public NotificationDto Notification { get; set; }
    }
}
