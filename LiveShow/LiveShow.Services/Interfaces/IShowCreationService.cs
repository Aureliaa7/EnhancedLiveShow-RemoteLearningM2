using LiveShow.Services.Models.Show;
using System.Threading.Tasks;

namespace LiveShow.Services.Interfaces
{
    public interface IShowCreationService
    {
        Task<ShowDto> CreateShow(ShowDtoWithoutId show);
    }
}
