using System;
using System.Collections.Generic;

namespace SocialMedia.Models
{
    public partial class MemberGroup
    {
        public int IdGroup { get; set; }
        public int IdUser { get; set; }

        public virtual Group IdGroupNavigation { get; set; } = null!;
        public virtual InfoUser IdUserNavigation { get; set; } = null!;
    }
}
