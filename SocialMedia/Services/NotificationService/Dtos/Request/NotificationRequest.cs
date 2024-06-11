namespace SocialMedia.Dtos.Requests
{
    public class NotificationRequest
    {
        public NotificationRequest(int IdUser,int TypeNotification,int IdUserRelative,int? IdItemRelative) {
            this.IdUser = IdUser;
            this.TypeNotification = TypeNotification;
            this.IdUserRelative = IdUserRelative;
            this.IdItemRelative = IdItemRelative;   
        
        }
        public int IdUser { get; set; }

        public int TypeNotification { get; set; }

        public int IdUserRelative { get; set; }

        public int? IdItemRelative { get; set; }
    }
}
