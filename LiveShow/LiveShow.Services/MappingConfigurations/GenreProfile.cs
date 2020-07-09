using AutoMapper;
using LiveShow.Dal.Models;
using LiveShow.Services.Models.Show;

namespace LiveShow.Services.MappingConfigurations
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, GenreDto>().ReverseMap();
        }
    }
}
