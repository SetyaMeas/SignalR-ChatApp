namespace ChatApp.Application.Commons.Interfaces
{
    public interface IPasswordHasher
    {
        public byte[] GenerateSalt(int size = 32);
        public string Hash(string password, byte[] salt);
        public bool Compare(string password, string hashedPassword, byte[] salt);
    }
}
