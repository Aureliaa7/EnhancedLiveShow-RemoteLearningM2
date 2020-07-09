using AutoMapper;
using LiveShow.Dal.Interfaces;
using LiveShow.Dal.Models;
using LiveShow.Services.Interfaces;
using LiveShow.Services.Models.Notification;
using LiveShow.Services.Models.Show;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LiveShow.Services.Services
{
    public class ShowNotificationService : IShowNotificationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ShowNotificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task NotifyFollowers(ShowDto show, NotificationTypeDto notificationType)
        {
            var notificationTYpe_ = mapper.Map<NotificationType>(notificationType);
             Notification notification = new Notification
            {
                DateTime = DateTime.Now,
                Type = notificationTYpe_,
                OriginalDateTime = show.DateTime,
                OriginalVenue = show.Venue,
                ShowId = show.Id,
            };
            await unitOfWork.NotificationRepository.Add(notification);
            var totalAttendances = await unitOfWork.AttendanceRepository.GetAll();
            foreach (var attendance in totalAttendances)
            {
                if (attendance.ShowId == show.Id)
                {
                    await unitOfWork.UserNotificationRepository.Add(new UserNotification { NotificationId = notification.Id, UserId = attendance.AttendeeId, IsRead = false });
                }
            }
        }

        public async Task NotifyCreatedShow(ShowDto show)
        {
            NotificationDto notification = new NotificationDto()
            {
                Id = Guid.NewGuid(),
                DateTime = DateTime.Now,
                Type = NotificationTypeDto.AddedShow,
                OriginalDateTime = show.DateTime,
                OriginalVenue = show.Venue,
                ShowId = show.Id,
            };
            var insertedNotification = await unitOfWork.NotificationRepository.Add(mapper.Map<Notification>(notification));

            var followings = await unitOfWork.FollowingRepository.Find(f => f.FolloweeId == show.ArtistId);
            var followers = from u in await unitOfWork.UserRepository.GetAll() join f in followings on u.Id equals f.FollowerId select u;

            foreach (var f in followers)
            {
                var userNotification = new UserNotification
                {
                    UserId = f.Id,
                    NotificationId = notification.Id,
                    IsRead = false
                };
                await unitOfWork.UserNotificationRepository.Add(userNotification);
            }
        }
    }
}
