using LiveShow.Services.Models.Show;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiveShow.Services.Interfaces
{
    public interface IShowGettingService
    {
        Task<ShowDto> GetShow(Guid showId);
        Task<IEnumerable<ShowDto>> GetAllShows();
        Task<IEnumerable<ShowDto>> GetShowsForArtist(Guid artistId);
        Task<IEnumerable<ShowDto>> GetShowsToBeAttended(Guid attendeeId);
        Task<IEnumerable<ShowDto>> FindShows(Guid attendeeId);
    }
}
