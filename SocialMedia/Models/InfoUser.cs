using System;
using System.Collections.Generic;

namespace SocialMedia.Models
{
    public partial class InfoUser
    {
        public InfoUser()
        {
            Posts = new HashSet<Post>();
        }

        public int IdUser { get; set; }
        public string UserName { get; set; } = null!;
        public string? UserDescription { get; set; }
        public string EmailUser { get; set; } = null!;
        public string PasswordUser { get; set; } = null!;
        public string? PhoneNumberUser { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
