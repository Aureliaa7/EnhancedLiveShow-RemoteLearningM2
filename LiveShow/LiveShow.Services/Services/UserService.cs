using AutoMapper;
using LiveShow.Dal.Interfaces;
using LiveShow.Dal.Models;
using LiveShow.Services.Exceptions;
using LiveShow.Services.Interfaces;
using LiveShow.Services.Models.User;
using LiveShow.Services.PasswordService;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LiveShow.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IAccountService accountService;
        private readonly IPasswordEncryption passwordEncryption;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IAccountService accountService, IPasswordEncryption passwordEncryption)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.accountService = accountService;
            this.passwordEncryption = passwordEncryption;
        }

        public async Task<UserDto> ChangeName(UserEditDto userEditDto)
        {
            bool userExists = await unitOfWork.UserRepository.Exists(u => u.Id == userEditDto.Id);
            if(userExists)
            {
                var user = await unitOfWork.UserRepository.Get(userEditDto.Id);
                user.LastName = userEditDto.LastName;
                user.FirstName = userEditDto.FirstName;
                var updatedUser = await unitOfWork.UserRepository.Update(user);
                return mapper.Map<UserDto>(updatedUser);
            }
            throw new ItemNotFoundException("The user was not found...");
        }

        public async Task<UserDto> ChangePassword(UserChangePasswordDto userDto)
        {
            bool userExists = await unitOfWork.UserRepository.Exists(u => u.Id == userDto.Id);
            if(userExists)
            {
                bool accountExists = await accountService.AccountExists(new UserLoginDto { Password = userDto.CurrentPassword, Username = userDto.Username });
                if(accountExists)
                {
                    var user = await unitOfWork.UserRepository.Get(userDto.Id);
                    user.Salt = Salt.Create();
                    var plainPassword = userDto.NewPassword;
                    user.Password = passwordEncryption.Encrypt(plainPassword, user.Salt);
                    var updatedUser = await unitOfWork.UserRepository.Update(user);
                    return mapper.Map<UserDto>(updatedUser);
                }
            }
            throw new ItemNotFoundException("The user was not found...");
        }

        public async Task<string> GetRole(string username)
        {
            var user = await unitOfWork.UserRepository.Find(u => u.Username.Equals(username));
            if (user != null) {
                return user.FirstOrDefault().Type == UserType.Artist ? "Artist" : "User";
            }
            throw new ItemNotFoundException("The user with the specified username was not found...");
        }

        public async Task<UserDto> GetUser(Guid id)
        {
            return mapper.Map<UserDto>(await unitOfWork.UserRepository.Get(id));
        }
    }
}
