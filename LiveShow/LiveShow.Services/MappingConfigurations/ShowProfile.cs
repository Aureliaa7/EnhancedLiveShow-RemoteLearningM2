using AutoMapper;
using LiveShow.Dal.Models;
using LiveShow.Services.Models.Show;

namespace LiveShow.Services.MappingConfigurations
{
    public class ShowProfile : Profile
    {
        public ShowProfile()
        {
            CreateMap<Show, ShowDto>();
            CreateMap<ShowDto, Show>();
        }
    }
}
