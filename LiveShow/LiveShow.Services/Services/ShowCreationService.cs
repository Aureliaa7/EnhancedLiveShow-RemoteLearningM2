using AutoMapper;
using LiveShow.Dal.Interfaces;
using LiveShow.Dal.Models;
using LiveShow.Services.Exceptions;
using LiveShow.Services.Interfaces;
using LiveShow.Services.Models.Show;
using System.Threading.Tasks;

namespace LiveShow.Services.Services
{
    public class ShowCreationService : IShowCreationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IShowNotificationService showNotification;

        public ShowCreationService(IUnitOfWork unitOfWork, IMapper mapper, IShowNotificationService showNotification)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.showNotification = showNotification;
        }

        public async Task<ShowDto> CreateShow(ShowDtoWithoutId show)
        {
            var artistExists = await unitOfWork.UserRepository.Exists(u => u.Id == show.ArtistId);
            var genreExists = await unitOfWork.GenreRepository.Exists(g => g.Id == show.GenreId);
            if (artistExists && genreExists)
            {
                show.IsCanceled = false;
                var createdShow = await unitOfWork.ShowRepository.Add(mapper.Map<Show>(show));
                await showNotification.NotifyCreatedShow(mapper.Map<ShowDto>(createdShow));
                return mapper.Map<ShowDto>(createdShow);
            }
            throw new ItemNotFoundException("The artist or the music genre was not found...");
        }
    }
}
