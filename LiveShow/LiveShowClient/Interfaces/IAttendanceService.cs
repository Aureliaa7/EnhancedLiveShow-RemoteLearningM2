using LiveShow.Services.Models.Attendance;
using System;
using System.Threading.Tasks;

namespace LiveShowClient.Interfaces
{
    public interface IAttendanceService
    {
        Task<string> AttendShow(AttendanceDto attendanceDto);
        Task<string> NeglectShow(Guid attendeeId, Guid showId);
        Task<int> GetNoAttendances(Guid showId);
    }
}
