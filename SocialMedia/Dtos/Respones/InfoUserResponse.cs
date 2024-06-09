using SocialMedia.Models;

namespace SocialMedia.Dtos.Respones
{
    public class InfoUserResponse
    {
        public int IdUser { get; set; }

        public string UserName { get; set; }

        public string? UserDescription { get; set; }

        public IEnumerable<PostResponse>? PostResponses { get; set; }

        public string? FriendStatus { get; set; }

        public bool isCurrentUser {  get; set; }

        public string AvatarImage { get; set; }

        public string CoverImage { get; set; }

    }
}
