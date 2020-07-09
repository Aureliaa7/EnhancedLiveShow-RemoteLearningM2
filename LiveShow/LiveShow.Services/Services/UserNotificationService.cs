using AutoMapper;
using LiveShow.Dal.Interfaces;
using LiveShow.Services.Exceptions;
using LiveShow.Services.Interfaces;
using LiveShow.Services.Models.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveShow.Services.Services
{
    public class UserNotificationService : IUserNotificationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserNotificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<UserNotificationDto>> GetAllNotifications(Guid userId)
        {
            if (await unitOfWork.UserRepository.Exists(u => u.Id == userId))
            {
                var userNotifications = await unitOfWork.UserNotificationRepository.Find(n => n.UserId == userId);
                var userNotificationDtos = new List<UserNotificationDto>();
                foreach(var notification in userNotifications)
                {
                    userNotificationDtos.Add(mapper.Map<UserNotificationDto>(notification));
                }
                return userNotificationDtos;
            }
            throw new ItemNotFoundException("The user was not found...");
        }

        public async Task<IEnumerable<UserNotificationDto>> GetUnreadNotifications(Guid userId)
        {
            if (await unitOfWork.UserRepository.Exists(u => u.Id == userId))
            {
                var userNotifications = await unitOfWork.UserNotificationRepository.Find(n => n.UserId == userId && n.IsRead == false);
                var userNotificationDtos = new List<UserNotificationDto>();
                foreach (var notification in userNotifications)
                {
                    userNotificationDtos.Add(mapper.Map<UserNotificationDto>(notification));
                }
                return userNotificationDtos;
            }
            throw new ItemNotFoundException("The user was not found...");
        }

        public async Task MarkNotificationAsRead(Guid notificationId, Guid userId)
        {
            if (await unitOfWork.UserNotificationRepository.Exists(n => n.NotificationId == notificationId))
            {
                var notification = (await unitOfWork.UserNotificationRepository.Find(n => n.NotificationId == notificationId && n.UserId == userId)).First();
                notification.IsRead = true;
                await unitOfWork.UserNotificationRepository.Update(notification);
            }
            throw new ItemNotFoundException("The user was not found...");
        }
    }
}
