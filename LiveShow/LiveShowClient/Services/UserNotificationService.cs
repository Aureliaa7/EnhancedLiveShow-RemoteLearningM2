using LiveShow.Services.Models.Notification;
using LiveShowClient.Interfaces;
using LiveShowClient.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiveShow.Services.ExtensionMethods;
using LiveShow.Services.Models.Show;

namespace LiveShowClient.Services
{
    public class UserNotificationService : IUserNotificationService
    {
        private readonly IApiService apiService;
        private readonly IUserService userService;
        private readonly IGenreService genreService;

        public UserNotificationService(IApiService apiService, IUserService userService, IGenreService genreService)
        {
            this.apiService = apiService;
            this.userService = userService;
            this.genreService = genreService;
        }

        public async Task<IEnumerable<UserNotificationVM>> GetNotifications(Guid id)
        {
            var userNotifications = await apiService.GetContentFromHttpAsync<IEnumerable<UserNotificationDto>>($"/usernotification/notifications/{id}");
            var notificationsVM = await MapNotifications(userNotifications);
            return notificationsVM;
        }

        public async Task<IEnumerable<UserNotificationVM>> GetUnreadNotifications(Guid id)
        {
            IEnumerable<UserNotificationVM> notificationsVM = new List<UserNotificationVM>();
            notificationsVM = await MapNotifications(await apiService.GetContentFromHttpAsync<IEnumerable<UserNotificationDto>>($"/usernotification/unreadnotifications/{id}"));
            return notificationsVM;
        }

        public async Task MarkNotificationAsRead(Guid id, Guid userId)
        {
            await apiService.PatchDataAsync($"/usernotification/{id}/{userId}", new JsonPatchDocument<UserNotificationDto>());
        }

        private async Task<IEnumerable<UserNotificationVM>> MapNotifications(IEnumerable<UserNotificationDto> userNotifications)
        {
            var notificationsVM = new List<UserNotificationVM>();
            foreach (UserNotificationDto notification in userNotifications)
            {
                var id = notification.NotificationId;
                var originalNotification = await apiService.GetContentFromHttpAsync<NotificationDto>($"/notification/notifications/{id}");
                var show = await apiService.GetContentFromHttpAsync<ShowDto>($"/show/{originalNotification.ShowId}");
                var showGenre = await genreService.GetGenre(show.GenreId);
                var artist = await userService.GetUser(show.ArtistId);
                var notificationVM = new UserNotificationVM
                {
                    NotificationId = id.ToString(), 
                    Info = $"{originalNotification.OriginalDateTime} { artist.FirstName + " " + artist.LastName} {originalNotification.Type.GetDescription()}, " +
                    $"genre: { showGenre.Name}, venue: {originalNotification.OriginalVenue}"
                };
                notificationsVM.Add(notificationVM);
            }
            return notificationsVM;
        }
    }
}
