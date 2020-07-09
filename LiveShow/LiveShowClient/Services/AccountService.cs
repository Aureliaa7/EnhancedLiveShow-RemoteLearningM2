using LiveShow.Services.Models.User;
using LiveShowClient.Interfaces;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace LiveShowClient.Services
{
    public class AccountService : IAccountService
    {
        private readonly IApiService apiService;

        public AccountService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<bool> AccountExists(UserLoginDto userLoginDto)
        {
            var result = await apiService.PostDataAsync("/account", userLoginDto);
            return JsonConvert.DeserializeObject<bool>(result);
        }

        public async Task<string> ChangePassword(UserChangePasswordDto userDto)
        {
            var result = await apiService.PutAsync("/user/changepassword", userDto);
            return result;
        }

        public async Task<string> EditName(UserEditDto userDto)
        {
            var result = await apiService.PutAsync("/user", userDto);
            return result;
        }

        public async Task<UserDto> Login(UserLoginDto userLogin)
        {
            var result = await apiService.PostDataAsync("/account/login", userLogin);
            return JsonConvert.DeserializeObject<UserDto>(result);
        }

        public async Task<string> Register(UserRegisterDto user)
        {
            var result = await apiService.PostDataAsync("/account/register", user);
            return result;
        }
    }
}
