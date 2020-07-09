using LiveShow.Services.Models.Show;
using LiveShowClient.ViewModels;
using System.Threading.Tasks;

namespace LiveShowClient.Interfaces
{
    public interface IShowCreationService
    {
        Task<ShowDto> CreateShow(ShowVM show);
    }
}
