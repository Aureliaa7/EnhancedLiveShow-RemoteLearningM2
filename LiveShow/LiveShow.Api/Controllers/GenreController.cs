using LiveShow.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LiveShow.Api.Controllers
{
    public class GenreController : LiveShowApiControllerBase
    {
        private readonly IGenreService genreService;

        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet("genres")]
        public async Task<IActionResult> GetGenres()
        {
            var shows = await genreService.GetMusicGenres();
            return Ok(shows);
        }

        [HttpGet("genres/{id}")]
        public async Task<IActionResult> GetGenres(byte id)
        {
            var shows = await genreService.GetGenre(id);
            return Ok(shows);
        }
    }
}
