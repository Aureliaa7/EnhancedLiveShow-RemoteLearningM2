using AutoMapper;
using LiveShow.Dal.Interfaces;
using LiveShow.Dal.Models;
using LiveShow.Services.Exceptions;
using LiveShow.Services.Interfaces;
using LiveShow.Services.Models.Show;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveShow.Services.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<GenreDto> GetGenre(byte id)
        {
            if(await unitOfWork.GenreRepository.Exists(g => g.Id == id))
            {
                Genre genre = (await unitOfWork.GenreRepository.Find(g => g.Id == id)).FirstOrDefault();
                GenreDto genreDto = mapper.Map<GenreDto>(genre);
                return genreDto;
            }
            throw new ItemNotFoundException("The genre does not exist...");
        }

        public async Task<IEnumerable<GenreDto>> GetMusicGenres()
        {
            var genres = await unitOfWork.GenreRepository.GetAll();
            var genresDto = new List<GenreDto>();
            foreach(var genre in genres)
            {
                genresDto.Add(mapper.Map<GenreDto>(genre));
            }
            return genresDto;
        }
    }
}
