using AutoMapper;
using LiveShow.Dal.Models;
using LiveShow.Services.Models.Notification;

namespace LiveShow.Services.MappingConfigurations
{
    public class NotificationTypeProfile : Profile
    {
        public NotificationTypeProfile()
        {
            CreateMap<NotificationType, NotificationTypeDto>().ReverseMap();
        }
    }
}
