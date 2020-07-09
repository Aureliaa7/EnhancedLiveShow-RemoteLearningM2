using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiveShow.Dal.Models
{
    public class Show
    {
        public Guid Id { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime DateTime { get; set; }
        [MaxLength(100)]
        public string Venue { get; set; }

        public Guid ArtistId { get; set; }
        public User Artist { get; set; }
        public byte GenreId { get; set; }
        public Genre Genre { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Attendance> Attendances { get; set; }

        public Show()
        {
            Notifications = new List<Notification>();
            Attendances = new List<Attendance>();
        }
    }
}
