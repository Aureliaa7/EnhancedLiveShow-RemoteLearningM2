using LiveShow.Services.Models.Attendance;
using LiveShowClient.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LiveShowClient.Controllers
{
    [Authorize]
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService attendanceService;
        private readonly IGettingCurrentUser currentUser;
        private readonly IHttpContextAccessor httpContext;

        public AttendanceController(IAttendanceService attendanceService, IGettingCurrentUser currentUser, IHttpContextAccessor httpContext)
        {
            this.attendanceService = attendanceService;
            this.currentUser = currentUser;
            this.httpContext = httpContext;
        }

        public async Task<IActionResult> AttendShow(Guid id)
        {
            var userId = currentUser.GetCurrentUserId(httpContext);
            var attendanceDto = new AttendanceDto { AttendeeId = userId, ShowId = id };
            await attendanceService.AttendShow(attendanceDto);
            return RedirectToAction("ShowsThatCanBeAttended", "Show");
        }

        public async Task<IActionResult> NeglectShow(Guid id)
        {
            var attendeeId = currentUser.GetCurrentUserId(httpContext);
            await attendanceService.NeglectShow(attendeeId, id);
            return RedirectToAction("ShowsToBeAttended", "Show");
        }
    }
}
