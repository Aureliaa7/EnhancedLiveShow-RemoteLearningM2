using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiveShow.Dal.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(100)]
        [Required]
        public string LastName { get; set; }

        [Required]
        public UserType Type { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
        public string Salt { get; set; }

        public ICollection<Following> Followers { get; set; }

        public ICollection<Following> Followees { get; set; }
        public ICollection<UserNotification> Notifications { get; set; }
        public ICollection<Show> Shows { get; set; }
        public ICollection<Attendance> Attendances { get; set; }

        public User()
        {
            Followers = new List<Following>();
            Followees = new List<Following>();
            Notifications = new List<UserNotification>();
            Shows = new List<Show>();
            Attendances = new List<Attendance>();
        }
    }
}
