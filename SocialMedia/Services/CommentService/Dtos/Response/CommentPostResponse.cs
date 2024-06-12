using SocialMedia.Dtos.Respones;

namespace SocialMedia.Services.CommentService.Dtos.Response
{
    public class CommentPostResponse
    {
        public int IdCommentPost { get; set; }
        public int IdPost { get; set; }
        public ItemSearchUser User { get; set; }
        public string ContentCommentPost { get; set; } = null!;
    }
}
