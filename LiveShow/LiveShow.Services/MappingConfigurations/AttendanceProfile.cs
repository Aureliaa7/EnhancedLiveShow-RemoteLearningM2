using AutoMapper;
using LiveShow.Dal.Models;
using LiveShow.Services.Models.Attendance;

namespace LiveShow.Services.MappingConfigurations
{
    public class AttendanceProfile : Profile
    {
        public AttendanceProfile()
        {
            CreateMap<Attendance, AttendanceDto>().ReverseMap();
        }
    }
}
