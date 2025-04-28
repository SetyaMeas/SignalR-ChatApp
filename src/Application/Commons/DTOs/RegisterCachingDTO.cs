namespace ChatApp.Application.Commons.DTOs
{
    public class RegisterCachingDTO
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
        public required string VerificationCode { get; init; }
        public required byte[] Salt { get; init; }
    }
}
