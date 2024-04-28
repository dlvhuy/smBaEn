namespace SocialMedia.Dtos.Requests
{
    public class CreateGroupRequest
    {
        public string GroupName { get; set; } = null!;
        public string? GroupAvatar { get; set; }
    }
}
