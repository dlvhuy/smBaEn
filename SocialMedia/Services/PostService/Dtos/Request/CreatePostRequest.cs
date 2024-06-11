using SocialMedia.Dtos.Requests;

namespace SocialMedia.Services.PostService.Dtos.Request
{
    public class CreatePostRequest
    {

        public int? IdGroup { get; set; }
        public string? PostContent { get; set; }
        public List<PostContentRequest>? PostContentRequests { get; set; }

    }
}
