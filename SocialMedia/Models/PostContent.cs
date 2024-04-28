using System;
using System.Collections.Generic;

namespace SocialMedia.Models
{
    public partial class PostContent
    {
        public int IdPostContent { get; set; }
        public int IdPost { get; set; }
        public string UrlimageVideo { get; set; } = null!;

        public virtual Post IdPostNavigation { get; set; } = null!;
    }
}
