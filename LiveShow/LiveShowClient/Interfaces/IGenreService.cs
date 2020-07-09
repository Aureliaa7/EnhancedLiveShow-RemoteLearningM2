using LiveShow.Services.Models.Show;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiveShowClient.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetMusicGenres();
        Task<GenreDto> GetGenre(byte id);
    }
}
