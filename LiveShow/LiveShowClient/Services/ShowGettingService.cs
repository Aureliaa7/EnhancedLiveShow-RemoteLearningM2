using LiveShow.Services.Models.Show;
using LiveShowClient.Interfaces;
using LiveShowClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveShowClient.Services
{
    public class ShowGettingService : IShowGettingService
    {
        private readonly IApiService apiService;
        private readonly IGenreService genreService;
        private readonly IUserService userService;
        private readonly IAttendanceService attendanceService;

        public ShowGettingService(IApiService apiService, IGenreService genreService, IUserService userService, IAttendanceService attendanceService)
        {
            this.apiService = apiService;
            this.genreService = genreService;
            this.userService = userService;
            this.attendanceService = attendanceService;
        }

        public async Task<ShowDto> GetShow(Guid showId)
        {
            return await apiService.GetContentFromHttpAsync<ShowDto>($"/show/{showId}");
        }

        public async Task<IEnumerable<ShowDto>> GetShows()
        {
            return await apiService.GetContentFromHttpAsync<List<ShowDto>>("/show/shows");
        }

        public async Task<IEnumerable<ShowWithIdVM>> GetShowsForArtist(Guid artistId)
        {
            var path = $"/show/shows/artist/{artistId}";
            var showsVM = await apiService.GetContentFromHttpAsync<IEnumerable<ShowDto>>(path);
            return await ConvertShowsToShowVM(showsVM);
        }

        public async Task<IEnumerable<ShowWithIdVM>> GetShowsToBeAttended(Guid attendeeId)
        {
            var showsVM = await apiService.GetContentFromHttpAsync<IEnumerable<ShowDto>>($"/show/shows/user/{attendeeId}");
            return await ConvertShowsToShowVM(showsVM);
        }

        public async Task<IEnumerable<ShowWithIdVM>> ShowsThatCanBeAttended(Guid attendeeId)
        {
            var result = await apiService.GetContentFromHttpAsync<IEnumerable<ShowDto>>($"/show/find/{attendeeId}");
            var showsVM = await ConvertShowsToShowVM(result);
            return showsVM;
        }

        private async Task<IEnumerable<ShowWithIdVM>> ConvertShowsToShowVM(IEnumerable<ShowDto> showDtos)
        {
            var showsVM = new List<ShowWithIdVM>();
            var genres = await genreService.GetMusicGenres();

            foreach (var show in showDtos)
            {
                var artist = await userService.GetUser(show.ArtistId);
                showsVM.Add(new ShowWithIdVM
                {
                    Id = show.Id,
                    ArtistId = show.ArtistId,
                    DateTime = show.DateTime,
                    Venue = show.Venue,
                    Genre = genres.Where(g => g.Id == show.GenreId).FirstOrDefault().Name,
                    IsCanceled = show.IsCanceled,
                    ArtistName = artist.FirstName + " " + artist.LastName,
                    Attendances = await attendanceService.GetNoAttendances(show.Id)
                });
            }
            return showsVM;
        }
    }
}
