namespace ChatApp.Application.Commons.Exceptions
{
    public class DuplicateEmailException : Exception
    {
        public DuplicateEmailException(string message)
            : base(message) { }
    }
}
