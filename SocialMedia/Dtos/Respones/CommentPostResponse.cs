namespace SocialMedia.Dtos.Respones
{
    public class CommentPostResponse
    {
        public int IdCommentPost { get; set; }
        public int IdPost { get; set; }
        public ItemSearchUser User { get; set; }
        public string ContentCommentPost { get; set; } = null!;
    }
}
