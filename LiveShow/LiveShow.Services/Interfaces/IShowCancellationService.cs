using LiveShow.Services.Models.Show;
using System;
using System.Threading.Tasks;

namespace LiveShow.Services.Interfaces
{
    public interface IShowCancellationService
    {
        Task<ShowDto> CancelShow(Guid id, Guid artistId);
    }
}
