using AutoMapper;
using LiveShow.Dal.Interfaces;
using LiveShow.Dal.Models;
using LiveShow.Services.Exceptions;
using LiveShow.Services.Interfaces;
using LiveShow.Services.Models.Notification;
using LiveShow.Services.Models.Show;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Threading.Tasks;

namespace LiveShow.Services.Services
{
    public class ShowUpdatingService : IShowUpdatingService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IShowNotificationService showNotification;

        public ShowUpdatingService(IUnitOfWork unitOfWork, IMapper mapper, IShowNotificationService showNotification)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.showNotification = showNotification;
        }
        
        public async Task<ShowDto> UpdateShow(Guid id, Guid artistId, JsonPatchDocument<ShowDto> showDto)
        {
            var artist = await unitOfWork.UserRepository.Get(artistId);
            var showToBeUpdated = mapper.Map<ShowDto>(await unitOfWork.ShowRepository.Get(id));
            showDto.ApplyTo(showToBeUpdated);
            if (artist != null && showToBeUpdated.ArtistId == artist.Id)
            {
                var updatedShow = await unitOfWork.ShowRepository.Update(mapper.Map<Show>(showToBeUpdated));
                await showNotification.NotifyFollowers(showToBeUpdated, NotificationTypeDto.UpdatedShow);
                return mapper.Map<ShowDto>(updatedShow);
            }
            throw new ItemNotFoundException("The user was not found...");
        }
    }
}
