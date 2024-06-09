namespace SocialMedia.Dtos.Respones
{
    public class NotificationResponse
    {
        public int IdNotification { get; set; }

        public string TypeNotification { get; set; }

        public string MessageNotification { get; set; }

        public string StatusNotification { get; set; }

        public ItemSearchUser UserInfo { get; set; }

        public int IdItemRelative { get; set; }
    }
}
