using AutoMapper;
using LiveShow.Dal.Interfaces;
using LiveShow.Dal.Models;
using LiveShow.Services.Exceptions;
using LiveShow.Services.Interfaces;
using LiveShow.Services.Models.Show;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveShow.Services.Services
{
    public class ShowGettingService : IShowGettingService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ShowGettingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ShowDto> GetShow(Guid showId)
        {   
            if(await unitOfWork.ShowRepository.Exists(s => s.Id == showId))
            {
                var show = await unitOfWork.ShowRepository.Get(showId);
                var showDto = mapper.Map<ShowDto>(show);
                return showDto;
            }
            throw new ItemNotFoundException("The show was not found...");
        }

        public async Task<IEnumerable<ShowDto>> GetAllShows()
        {
            var shows = await unitOfWork.ShowRepository.GetAll();
            var dtoShows = ConvertFromShowCollectionToShowDto(shows);
            return dtoShows;
        }

        public async Task<IEnumerable<ShowDto>> GetShowsForArtist(Guid artistId)
        {
            if(await unitOfWork.UserRepository.Exists(u => u.Id == artistId))
            {
                var shows = await unitOfWork.ShowRepository.Find(s => s.ArtistId == artistId);
                var dtoShows = ConvertFromShowCollectionToShowDto(shows);
                return dtoShows;
            }
            throw new ItemNotFoundException("The user was not found...");
        }

        public async Task<IEnumerable<ShowDto>> GetShowsToBeAttended(Guid attendeeId)
        {
            if(await unitOfWork.UserRepository.Exists(u => u.Id == attendeeId))
            {
                var dtoShows = new List<ShowDto>();
                var attendances = await unitOfWork.AttendanceRepository.Find(a => a.AttendeeId == attendeeId);
                foreach (var attendance in attendances)
                {
                    var show = (await unitOfWork.ShowRepository.Find(s => s.Id == attendance.ShowId && s.IsCanceled == false)).FirstOrDefault();
                    if (show != null)
                    {
                        dtoShows.Add(mapper.Map<ShowDto>(show));
                    }
                }
                return dtoShows;
            }
            throw new ItemNotFoundException("The user was not found...");
        }

        public async Task<IEnumerable<ShowDto>> FindShows(Guid attendeeId)
        {
            bool userExists = await unitOfWork.UserRepository.Exists(u => u.Id == attendeeId);
            if (userExists)
            {
                var attendances = await unitOfWork.AttendanceRepository.Find(a => a.AttendeeId == attendeeId);
                var shows = await unitOfWork.ShowRepository.GetAll();
                var dtoShows = ConvertFromShowCollectionToShowDto(shows).ToList();
                var auxShows = new List<ShowDto>();
                foreach (var s in dtoShows)
                {
                    if (!(s.IsCanceled) && !(DateTime.Now > s.DateTime))
                    {
                        auxShows.Add(s);
                    }
                }
                foreach (var attendance in attendances)
                {
                    auxShows.Remove(auxShows.Where(s => s.Id == attendance.ShowId).FirstOrDefault());
                }
                return auxShows;
            }
            throw new ItemNotFoundException("The user was not found...");
        }

        private IEnumerable<ShowDto> ConvertFromShowCollectionToShowDto(IEnumerable<Show> shows)
        {
            var dtoShows = new List<ShowDto>();

            foreach (var show in shows)
            {
                dtoShows.Add(mapper.Map<ShowDto>(show));
            }
            return dtoShows;
        }
    }
}
