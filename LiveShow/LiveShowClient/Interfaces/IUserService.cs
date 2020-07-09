using LiveShow.Services.Models.User;
using System;
using System.Threading.Tasks;

namespace LiveShowClient.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUser(Guid id);
        Task<UserEditDto> GetUserToBeEdited(Guid id);
    }
}
