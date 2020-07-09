using LiveShow.Services.Models.Show;
using System;
using System.Threading.Tasks;

namespace LiveShowClient.Interfaces
{
    public interface IShowCancellationService
    {
        Task<ShowDto> CancelShow(Guid id, Guid artistId);
    }
}
