using LiveShow.Services.Models.Attendance;
using System;
using System.Threading.Tasks;

namespace LiveShow.Services.Interfaces
{
    public interface IShowAttendanceService
    {
        Task<AttendanceDto> AttendShow(Guid attendeeId, Guid showId);
        Task NeglectShow(Guid attendeeId, Guid showId);
        Task<int> GetAttendances(Guid showId);
    }
}
