using System;
using System.Collections.Generic;

namespace SocialMedia.Models
{
    public partial class CommentPost
    {
        public int IdCommentPost {  get; set; }
        public int IdPost { get; set; }
        public int IdUser { get; set; }
        public string ContentCommentPost { get; set; } = null!;

        public virtual Post IdPostNavigation { get; set; } = null!;
        public virtual InfoUser IdUserNavigation { get; set; } = null!;
    }
}
