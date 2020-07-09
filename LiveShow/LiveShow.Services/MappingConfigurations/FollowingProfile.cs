using AutoMapper;
using LiveShow.Dal.Models;
using LiveShow.Services.Models.Following;

namespace LiveShow.Services.MappingConfigurations
{
    public class FollowingProfile : Profile
    {
        public FollowingProfile()
        {
            CreateMap<Following, FollowingDto>();
            CreateMap<FollowingDto, Following>();
        }
    }
}
