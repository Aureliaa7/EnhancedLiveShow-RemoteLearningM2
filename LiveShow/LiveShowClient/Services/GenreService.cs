using LiveShow.Services.Models.Show;
using LiveShowClient.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiveShowClient.Services
{
    public class GenreService : IGenreService
    {
        private readonly IApiService apiService;

        public GenreService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<GenreDto> GetGenre(byte id)
        {
            return await apiService.GetContentFromHttpAsync<GenreDto>($"/genre/genres/{id}");
        }

        public async Task<IEnumerable<GenreDto>> GetMusicGenres()
        {
            var musicGenres = await apiService.GetContentFromHttpAsync<List<GenreDto>>("/genre/genres");
            return musicGenres;
        }
    }
}
