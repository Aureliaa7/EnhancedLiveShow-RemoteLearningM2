using System;
using System.Threading.Tasks;

namespace LiveShow.Services.Interfaces
{
    public interface IShowDeletionService
    {
        Task DeleteShow(Guid id, Guid artistId);
    }
}
