using System;

namespace LiveShow.Dal.Models
{
    public  class Attendance
    {
        public Guid ShowId { get; set; }
        public Guid AttendeeId { get; set; }

        public Show Show { get; set; }
        public User Attendee { get; set; }
    }
}
