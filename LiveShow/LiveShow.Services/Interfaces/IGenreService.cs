using LiveShow.Services.Models.Show;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiveShow.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetMusicGenres();
        Task<GenreDto> GetGenre(byte id);
    }
}
