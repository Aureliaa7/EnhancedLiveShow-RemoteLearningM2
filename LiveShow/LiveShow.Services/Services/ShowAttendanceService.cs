using AutoMapper;
using LiveShow.Dal.Interfaces;
using LiveShow.Dal.Models;
using LiveShow.Services.Exceptions;
using LiveShow.Services.Interfaces;
using LiveShow.Services.Models.Attendance;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LiveShow.Services.Services
{
    public class ShowAttendanceService : IShowAttendanceService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ShowAttendanceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<AttendanceDto> AttendShow(Guid attendeeId, Guid showId)
        {
            var userExists = await unitOfWork.UserRepository.Exists(u => u.Id == attendeeId);
            var showExists = await unitOfWork.ShowRepository.Exists(s => s.Id == showId);
            if (userExists && showExists)
            {
                var attendance = await unitOfWork.AttendanceRepository.Add(new Attendance { ShowId = showId, AttendeeId = attendeeId });
                return mapper.Map<AttendanceDto>(attendance);
            }
            throw new ItemNotFoundException("The user or the show does not exist...");
        }

        public async Task<int> GetAttendances(Guid showId)
        {
            var showExists = await unitOfWork.ShowRepository.Exists(s => s.Id == showId);
            if(showExists)
            {
                var attendances = await unitOfWork.AttendanceRepository.Find(a => a.ShowId == showId);
                return attendances.Count();
            }
            throw new ItemNotFoundException("The show does not exist...");
        }

        public async Task NeglectShow(Guid attendeeId, Guid showId)
        {
            var attendanceExists = await unitOfWork.AttendanceRepository.Exists(a => a.AttendeeId == attendeeId && a.ShowId == showId);
            if (attendanceExists)
            {
                var attendance = await unitOfWork.AttendanceRepository.Find(a => a.AttendeeId == attendeeId && a.ShowId == showId);
                await unitOfWork.AttendanceRepository.RemoveRange(attendance);
            }
            throw new ItemNotFoundException("The user or the show does not exist...");
        }
    }
}
