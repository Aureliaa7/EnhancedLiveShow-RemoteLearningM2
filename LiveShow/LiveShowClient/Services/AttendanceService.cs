using LiveShow.Services.Models.Attendance;
using LiveShowClient.Interfaces;
using System;
using System.Threading.Tasks;

namespace LiveShowClient.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IApiService apiService;

        public AttendanceService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<string> AttendShow(AttendanceDto attendanceDto)
        {
            return await apiService.PostDataAsync("/attendance", attendanceDto);
        }

        public async Task<int> GetNoAttendances(Guid showId)
        {
            var noAttendances = await apiService.GetContentFromHttpAsync<int>($"/attendance/attendances/{showId}");
            return noAttendances;
        }

        public async Task<string> NeglectShow(Guid attendeeId, Guid showId)
        {
            return await apiService.DeleteDataAsync($"/attendance/{attendeeId}/{showId}");
        }
    }
}
