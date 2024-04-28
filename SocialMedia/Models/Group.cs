using System;
using System.Collections.Generic;

namespace SocialMedia.Models
{
    public partial class Group
    {
        public Group()
        {
            Posts = new HashSet<Post>();
        }

        public int IdGroup { get; set; }
        public string GroupName { get; set; } = null!;
        public string? GroupAvatar { get; set; }
        public int IdUser { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
