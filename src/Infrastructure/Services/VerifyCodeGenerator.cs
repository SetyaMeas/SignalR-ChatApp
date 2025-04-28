using System.Text;
using ChatApp.Application.Commons.Interfaces;

namespace ChatApp.Infrastucture.Services
{
    public class VerifyCodeGenerator : IVerifyCodeGenerator
    {
        public string Generate(int size = 6)
        {
            StringBuilder verificationCode = new(size);
            Random ran = new();

            for (int i = 0; i < size; i++)
            {
                int ranNumber = ran.Next(0, 9);
                verificationCode.Append(ranNumber);
            }
            return verificationCode.ToString();
        }
    }
}
