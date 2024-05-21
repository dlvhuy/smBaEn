namespace SocialMedia.Dtos.Requests
{
    public class PostRequest
    {
        public int IdUser { get; set; }
        public int? IdGroup { get; set; }
        public string? PostContent { get; set; }

    }
}
