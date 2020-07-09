using AutoMapper;
using LiveShow.Dal.Models;
using LiveShow.Services.Models.User;

namespace LiveShow.Services.MappingConfigurations
{
    public class UserRegistrationProfile : Profile
    {
        public UserRegistrationProfile()
        {
            CreateMap<User, UserRegisterDto>();
            CreateMap<UserRegisterDto, User>();
        }
    }
}
