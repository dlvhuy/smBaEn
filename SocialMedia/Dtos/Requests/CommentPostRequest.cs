namespace SocialMedia.Dtos.Requests
{
    public class CommentPostRequest
    {
        public int IdPost { get; set; }

        public string ContentCommentPost { get; set; } = null!;
    }
}
