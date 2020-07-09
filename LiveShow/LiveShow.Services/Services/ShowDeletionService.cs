using LiveShow.Dal.Interfaces;
using LiveShow.Services.Exceptions;
using LiveShow.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace LiveShow.Services.Services
{
    public class ShowDeletionService : IShowDeletionService
    {
        private readonly IUnitOfWork unitOfWork;

        public ShowDeletionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task DeleteShow(Guid id, Guid artistId)
        {
            var showExists = await unitOfWork.ShowRepository.Exists(s => s.Id == id && s.ArtistId == artistId);
            var artistExists = await unitOfWork.UserRepository.Exists(f => f.Id == artistId);

            if (showExists && artistExists)
            {
                await unitOfWork.ShowRepository.Remove(id);
            }
            else
            {
                throw new ItemNotFoundException("The artist or the show was not found...");
            }
        }
    }
}
