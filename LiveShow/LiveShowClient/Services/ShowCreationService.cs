using LiveShow.Dal.Models;
using LiveShow.Services.Models.Show;
using LiveShowClient.Interfaces;
using LiveShowClient.ViewModels;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace LiveShowClient.Services
{
    public class ShowCreationService : IShowCreationService
    {
        private readonly IGenreService genreService;
        private readonly IApiService apiService;

        public ShowCreationService(IGenreService genreService, IApiService apiService)
        {
            this.genreService = genreService;
            this.apiService = apiService;
        }
        public async Task<ShowDto> CreateShow(ShowVM show)
        {
            var genres = await genreService.GetMusicGenres();
            var genre = genres.Where(g => g.Name == show.Genre).FirstOrDefault();
            var showDto = new ShowDtoWithoutId { ArtistId = show.ArtistId, DateTime = show.DateTime, GenreId = genre.Id, IsCanceled = false, Venue = show.Venue };
            var result = await apiService.PostDataAsync("/show/shows", showDto);
            return JsonConvert.DeserializeObject<ShowDto>(result);
        }
    }
}
