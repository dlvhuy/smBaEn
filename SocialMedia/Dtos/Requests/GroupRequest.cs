namespace SocialMedia.Dtos.Requests
{
    public class GroupRequest
    {
        public string GroupName { get; set; } = null!;
        public IFormFile? GroupAvatar { get; set; }
    }
}
