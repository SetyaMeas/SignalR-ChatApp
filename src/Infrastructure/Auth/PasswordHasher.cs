using System.Security.Cryptography;
using System.Text;
using ChatApp.Application.Commons.Interfaces;

namespace ChatApp.Infrastucture.Auth
{
    public class PasswordHasher : IPasswordHasher
    {
        public byte[] GenerateSalt(int size = 32)
        {
            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[size];
            rng.GetBytes(salt);
            return salt;
        }

        public string Hash(string password, byte[] salt)
        {
            using var sha256 = SHA256.Create();
            var inputBytes = Encoding.UTF8.GetBytes(password);
            var saltedInput = new byte[inputBytes.Length + salt.Length];

            Buffer.BlockCopy(inputBytes, 0, saltedInput, 0, inputBytes.Length);
            Buffer.BlockCopy(salt, 0, saltedInput, inputBytes.Length, salt.Length);

            var hashBytes = sha256.ComputeHash(saltedInput);

            return Convert.ToHexStringLower(hashBytes);
        }

        public bool Compare(string password, string hashedPassword, byte[] salt)
        {
            return Hash(password, salt) == hashedPassword;
        }
    }
}
