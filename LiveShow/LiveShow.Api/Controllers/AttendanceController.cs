using LiveShow.Api.Filters;
using LiveShow.Services.Interfaces;
using LiveShow.Services.Models.Attendance;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LiveShow.Api.Controllers
{
    public class AttendanceController : LiveShowApiControllerBase
    {
        private readonly IShowAttendanceService showAttendance;

        public AttendanceController(IShowAttendanceService showAttendance)
        {
            this.showAttendance = showAttendance;
        }

        [HttpPost]
        [ModelValidationFilter]
        public async Task<IActionResult> AttendShow([FromBody] AttendanceDto attendance)
        {
            var createdAttendance = await showAttendance.AttendShow(attendance.AttendeeId, attendance.ShowId);
            return Created(Url.Action("AttendShow"), createdAttendance);
        }

        [HttpDelete("{attendeeId}/{showId}")]
        public async Task<IActionResult> NeglectShow(Guid attendeeId, Guid showId)
        {
            await showAttendance.NeglectShow(attendeeId, showId);
            return NoContent();
        }

        [HttpGet("attendances/{showId}")]
        public async Task<IActionResult> Attendances(Guid showId)
        {
            var attendances = await showAttendance.GetAttendances(showId);
            return Ok(attendances);
        }
    }
}
