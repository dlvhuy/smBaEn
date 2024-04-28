namespace SocialMedia.Dtos.Requests
{
    public class SignUpRequest
    {
        public string UserName { get; set; } = null!;
        public string EmailUser { get; set; } = null!;
        public string PasswordUser { get; set; } = null!;
        public string? PhoneNumberUser { get; set; }
    }
}
