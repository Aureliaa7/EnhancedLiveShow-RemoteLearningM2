using AutoMapper;
using LiveShow.Dal.Models;
using LiveShow.Services.Models.User;

namespace LiveShow.Services.MappingConfigurations
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
