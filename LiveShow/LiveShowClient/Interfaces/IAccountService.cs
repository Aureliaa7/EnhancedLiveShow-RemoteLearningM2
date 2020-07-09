using LiveShow.Services.Models.User;
using System.Threading.Tasks;

namespace LiveShowClient.Interfaces
{
    public interface IAccountService
    {
        Task<string> Register(UserRegisterDto userRegister);
        Task<UserDto> Login(UserLoginDto userLogin);
        Task<bool> AccountExists(UserLoginDto userLoginDto);
        Task<string> EditName(UserEditDto userDto);
        Task<string> ChangePassword(UserChangePasswordDto userDto);
    }
}
