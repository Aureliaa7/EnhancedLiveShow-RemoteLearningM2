using AutoMapper;
using LiveShow.Dal.Interfaces;
using LiveShow.Dal.Models;
using LiveShow.Services.Exceptions;
using LiveShow.Services.Interfaces;
using LiveShow.Services.Models.Notification;
using LiveShow.Services.Models.Show;
using System;
using System.Threading.Tasks;

namespace LiveShow.Services.Services
{
    public class ShowCancellationService : IShowCancellationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IShowNotificationService showNotification;

        public ShowCancellationService(IUnitOfWork unitOfWork, IMapper mapper, IShowNotificationService showNotification)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.showNotification = showNotification;
        }

        public async Task<ShowDto> CancelShow(Guid id, Guid artistId)
        {
            var showExists = await unitOfWork.ShowRepository.Exists(s => s.Id == id && s.ArtistId == artistId);
            var artistExists = await unitOfWork.UserRepository.Exists(f => f.Id == artistId);

            if (showExists && artistExists)
            {
                var showDto = mapper.Map<ShowDto>(await unitOfWork.ShowRepository.Get(id));
                var artist = await unitOfWork.UserRepository.Get(artistId);
                if (artist.Id == showDto.ArtistId)
                {
                    showDto.IsCanceled = true;
                    var canceledShow = await unitOfWork.ShowRepository.Update(mapper.Map<Show>(showDto));
                    await showNotification.NotifyFollowers(showDto, NotificationTypeDto.CanceledShow);
                    return mapper.Map<ShowDto>(canceledShow);
                }
                else
                {
                    throw new ItemNotFoundException("The user was not found...");
                }
            }
            else
            {
                throw new ItemNotFoundException("The show was not found...");
            }
        }
    }
}
