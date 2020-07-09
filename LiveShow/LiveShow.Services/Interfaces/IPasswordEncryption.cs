namespace LiveShow.Services.Interfaces
{
    public interface IPasswordEncryption
    {
        public string Encrypt(string password, string salt);
    }
}
