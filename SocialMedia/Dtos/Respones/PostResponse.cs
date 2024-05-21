namespace SocialMedia.Dtos.Respones
{
    public class PostResponse
    {
        public int IdPost { get; set; }
        public ItemSearchUser User { get; set; }
        public int? IdGroup { get; set; }
        public string? PostContent { get; set; }

        public LikePostResponse LikePost { get; set; }

        //public IEnumerable<PostContentResponse> postContentResponses { get; set; }
    }
}
