namespace SocialMedia.Services.CommentService.Dtos.Request
{
    public class CommentPostRequest
    {
        public int IdPost { get; set; }

        public string ContentCommentPost { get; set; } = null!;
    }
}
