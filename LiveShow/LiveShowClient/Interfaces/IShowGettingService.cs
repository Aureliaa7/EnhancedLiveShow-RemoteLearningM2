using LiveShow.Services.Models.Show;
using LiveShowClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiveShowClient.Interfaces
{
    public interface IShowGettingService
    {
        Task<IEnumerable<ShowDto>> GetShows();
        Task<ShowDto> GetShow(Guid showId);
        Task<IEnumerable<ShowWithIdVM>> GetShowsForArtist(Guid artistId);
        Task<IEnumerable<ShowWithIdVM>> GetShowsToBeAttended(Guid attendeeId);
        Task<IEnumerable<ShowWithIdVM>> ShowsThatCanBeAttended(Guid attendeeId);
    }
}
