using LiveShow.Services.Models.Show;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Threading.Tasks;

namespace LiveShow.Services.Interfaces
{
    public interface IShowUpdatingService
    {
        Task<ShowDto> UpdateShow(Guid id, Guid artistId, JsonPatchDocument<ShowDto> showDto);
    }
}
