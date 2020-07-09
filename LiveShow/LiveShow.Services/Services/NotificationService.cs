using AutoMapper;
using LiveShow.Dal.Interfaces;
using LiveShow.Services.Exceptions;
using LiveShow.Services.Interfaces;
using LiveShow.Services.Models.Notification;
using System;
using System.Threading.Tasks;

namespace LiveShow.Services.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<NotificationDto> GetNotification(Guid id)
        {
            if (await unitOfWork.NotificationRepository.Exists(n => n.Id == id))
            {
                var notification = await unitOfWork.NotificationRepository.Get(id);
                return mapper.Map<NotificationDto>(notification);
            }
            throw new ItemNotFoundException("The notification does not exist...");
        }
    }
}
