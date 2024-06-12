namespace SocialMedia.Services.CommentService.Dtos.Request
{
    public class GroupCommentPostRequest
    {
        public int idPost { get; set; }

        public bool isOpen { get; set; }
    }
}
