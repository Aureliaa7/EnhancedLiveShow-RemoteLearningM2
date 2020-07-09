using System;
using System.Threading.Tasks;

namespace LiveShowClient.Interfaces
{
    public interface IShowDeletionService
    {
        Task DeleteShow(Guid id, Guid artistId);
    }
}
