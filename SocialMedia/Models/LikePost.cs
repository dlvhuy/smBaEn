using System;
using System.Collections.Generic;

namespace SocialMedia.Models
{
    public partial class LikePost
    {
        public int IdLikePost {  get; set; }
        public int IdPost { get; set; }
        public int IdUser { get; set; }

        public virtual Post IdPostNavigation { get; set; } = null!;
        public virtual InfoUser IdUserNavigation { get; set; } = null!;
    }
}
