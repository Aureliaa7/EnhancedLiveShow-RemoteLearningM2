using LiveShow.Services.Models.User;
using System;
using System.Threading.Tasks;

namespace LiveShow.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> GetRole(string username);
        Task<UserDto> GetUser(Guid id);
        Task<UserDto> ChangeName(UserEditDto userEditDto);
        Task<UserDto> ChangePassword(UserChangePasswordDto userDto);
    }
}