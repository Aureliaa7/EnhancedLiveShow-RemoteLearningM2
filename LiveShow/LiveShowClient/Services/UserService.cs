using LiveShow.Services.Models.User;
using LiveShowClient.Interfaces;
using System;
using System.Threading.Tasks;

namespace LiveShowClient.Services
{
    public class UserService : IUserService
    {
        private readonly IApiService apiService;

        public UserService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<UserDto> GetUser(Guid id)
        {
            return await apiService.GetContentFromHttpAsync<UserDto>($"/user/{id}");
        }

        public async Task<UserEditDto> GetUserToBeEdited(Guid id)
        {
            var user = await GetUser(id);
            UserEditDto userEditDto = new UserEditDto
            {
                Id = id,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return userEditDto;
        }
    }
}
