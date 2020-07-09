using LiveShow.Services.Models.Show;
using LiveShowClient.ViewModels;
using System;
using System.Threading.Tasks;

namespace LiveShowClient.Interfaces
{
    public interface IShowUpdatingService
    {
        Task<ShowDto> UpdateShow(Guid id, Guid artistId, ShowUpdatingVM showDto);
    }
}
