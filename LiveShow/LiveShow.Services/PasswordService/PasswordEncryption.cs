using LiveShow.Services.Interfaces;

namespace LiveShow.Services.PasswordService
{
    public class PasswordEncryption : IPasswordEncryption
    {
        public string Encrypt(string password, string salt)
        {
            return Hash.Create(password, salt);
        }
    }
}
