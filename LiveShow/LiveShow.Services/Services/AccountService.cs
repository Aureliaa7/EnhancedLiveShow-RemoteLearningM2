using AutoMapper;
using LiveShow.Dal.Interfaces;
using LiveShow.Dal.Models;
using LiveShow.Services.Exceptions;
using LiveShow.Services.Interfaces;
using LiveShow.Services.Models.User;
using LiveShow.Services.PasswordService;
using System.Linq;
using System.Threading.Tasks;

namespace LiveShow.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IPasswordEncryption passwordEncryption;
        private readonly IPasswordValidation passwordValidation;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordEncryption passwordEncryption, IPasswordValidation passwordValidation)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.passwordEncryption = passwordEncryption;
            this.passwordValidation = passwordValidation;
        }

        public async Task<bool> AccountExists(UserLoginDto userLoginDto)
        {
            bool validPassword = false;
            bool accountExists = false;
            var userExists = await unitOfWork.UserRepository.Exists(u => u.Username.Equals(userLoginDto.Username));
            if (userExists)
            {
                var user = (await unitOfWork.UserRepository.Find(u => u.Username == userLoginDto.Username)).FirstOrDefault();
                validPassword = passwordValidation.IsValid(userLoginDto.Password, user.Salt, user.Password);
                accountExists = validPassword;
            }
            return accountExists;
        }

        public async Task<UserDto> Login(UserLoginDto userLogin)
        {
            UserDto userDto = null;
            if (await AccountExists(userLogin))
            {
                var user = (await unitOfWork.UserRepository.Find(u => u.Username == userLogin.Username)).FirstOrDefault();
                userDto = mapper.Map<UserDto>(user);
                return userDto;
            }
            throw new ItemNotFoundException("The account does not exist...");
        }

        public async Task<UserDto> Register(UserRegisterDto userDto)
        {
            userDto.Salt = Salt.Create();
            var originalPassword = userDto.Password;
            userDto.Password = passwordEncryption.Encrypt(originalPassword, userDto.Salt);
            User user = mapper.Map<User>(userDto);
            var registeredUser = mapper.Map<UserDto>(await unitOfWork.UserRepository.Add(user));
            return registeredUser;
        }
    }
}
