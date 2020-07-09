using LiveShow.Services.Models.User;
using System.Threading.Tasks;

namespace LiveShow.Services.Interfaces
{
    public interface IAccountService
    {
        Task<UserDto> Login(UserLoginDto userLoginDto);
        Task<UserDto> Register(UserRegisterDto userRegisterDto);
        Task<bool> AccountExists(UserLoginDto userLoginDto);
    }
}
