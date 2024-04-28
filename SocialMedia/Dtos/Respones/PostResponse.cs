namespace SocialMedia.Dtos.Respones
{
    public class PostResponse
    {
        public int IdPost { get; set; }
        public int IdUser { get; set; }
        public int? IdGroup { get; set; }
        public string? PostContent { get; set; }

        //public IEnumerable<PostContentResponse> postContentResponses { get; set; }
    }
}
