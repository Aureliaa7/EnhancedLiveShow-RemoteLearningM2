using AutoMapper;
using LiveShow.Dal.Models;
using LiveShow.Services.Models.Notification;

namespace LiveShow.Services.MappingConfigurations
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationDto>().ReverseMap();
        }
    }
}
