namespace ChatApp.Application.Commons.Interfaces
{
    public interface IVerifyCodeGenerator
    {
        string Generate(int size = 6);
    }
}
