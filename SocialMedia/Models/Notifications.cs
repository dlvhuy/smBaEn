using Microsoft.AspNetCore.Identity;

namespace SocialMedia.Models
{
    public class Notifications
    {
        public int IdNotification { get; set; }

        public int IdUser { get; set; }

        public string TypeNotification { get; set; }

        public string MessageNotification { get; set; }

        public string StatusNotification { get; set; }

        public int IdUserRelative {  get; set; }
    
        public int? IdItemRelative { get; set; }

        public virtual InfoUser User { get; set; }

        public virtual InfoUser UserRelative { get; set; }

        public virtual Post Post { get; set; }


    }
}
