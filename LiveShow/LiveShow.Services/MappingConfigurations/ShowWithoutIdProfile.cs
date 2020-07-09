using AutoMapper;
using LiveShow.Dal.Models;
using LiveShow.Services.Models.Show;

namespace LiveShow.Services.MappingConfigurations
{
    public class ShowWithoutIdProfile : Profile
    {
        public ShowWithoutIdProfile()
        {
            CreateMap<Show, ShowDtoWithoutId>();
            CreateMap<ShowDtoWithoutId, Show>();
        }
    }
}
