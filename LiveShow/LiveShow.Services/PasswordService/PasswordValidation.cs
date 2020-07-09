using LiveShow.Services.Interfaces;

namespace LiveShow.Services.PasswordService
{
    public class PasswordValidation : IPasswordValidation
    {
        private readonly IPasswordEncryption passwordEncryption;
        public PasswordValidation(IPasswordEncryption passwordEncryption)
        {
            this.passwordEncryption = passwordEncryption;
        }

        public bool IsValid(string password, string salt, string encryptedPassword)
        {
            return (passwordEncryption.Encrypt(password, salt) == encryptedPassword);
        }
    }
}
