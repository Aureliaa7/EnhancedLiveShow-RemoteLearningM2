using AutoMapper;
using LiveShow.Dal.Models;
using LiveShow.Services.Models.Notification;

namespace LiveShow.Services.MappingConfigurations
{
    public class UserNotificationProfile : Profile
    {
        public UserNotificationProfile()
        {
            CreateMap<UserNotification, UserNotificationDto>().ReverseMap();
        }
    }
}
