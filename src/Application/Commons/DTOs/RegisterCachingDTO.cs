namespace ChatApp.Application.Commons.DTOs
{
    public class RegisterCachingDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string VerificationCode { get; set; }
    }
}
