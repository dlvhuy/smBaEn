namespace SocialMedia.Dtos.Requests
{
    public class NotificationFriendRequest
    {
        public NotificationFriendRequest(int IdUser, int TypeNotification, int IdUserRelative)
        {
            this.IdUser = IdUser;
            this.TypeNotification = TypeNotification;
            this.IdUserRelative = IdUserRelative;

        }
        public int IdUser { get; set; }

        public int TypeNotification { get; set; }

        public int IdUserRelative { get; set; }

    }
}
