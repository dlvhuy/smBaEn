using System;
using System.Collections.Generic;

namespace SocialMedia.Models
{
    public partial class Post
    {
        public int IdPost { get; set; }
        public int IdUser { get; set; }
        public int? IdGroup { get; set; }
        public string? PostContent { get; set; }
        public virtual List<PostContent>? PostImageContents { get; set; }

        public virtual Group? IdGroupNavigation { get; set; }
        public virtual InfoUser IdUserNavigation { get; set; } = null!;
    }
}
