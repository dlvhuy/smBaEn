namespace SocialMedia.Dtos.Respones
{
    public class ResonseLogin
    {
        public int idCurrentUser { get; set; }
        public bool success { get; set; }

        public string Token { get; set; }

        public string? Message { get; set; }
    }
}
