namespace SocialMedia.Dtos.Requests
{
    public class PostContentRequest
    {
        public int IdPost { get; set; }
        public IFormFile UrlimageVideo { get; set; } = null!;
    }
}
