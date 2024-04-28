namespace SocialMedia.Dtos.Requests
{
    public class CreateserInfoRequest
    {
        public string UserName { get; set; } = null!;
        public string? UserDescription { get; set; }
        public string EmailUser { get; set; } = null!;
        public string PasswordUser { get; set; } = null!;
        public string? PhoneNumberUser { get; set; }
    }
}
