namespace LiveShow.Services.Interfaces
{
    public interface IPasswordValidation
    {
        bool IsValid(string password, string salt, string encryptedPassword);
    }
}
