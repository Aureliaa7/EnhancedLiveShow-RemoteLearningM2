using System;

namespace LiveShow.Services.Models.Attendance
{
    public class AttendanceDto
    {
        public Guid ShowId { get; set; }
        public Guid AttendeeId { get; set; }
    }
}
