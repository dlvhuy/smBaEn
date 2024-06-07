namespace SocialMedia.Models
{
    public partial class Friends
    {
        public int Id { get; set; }

        public int IdUser { get; set; }

        public int IdFriend { get; set; }

        public string Status { get; set; }

        public virtual InfoUser User { get; set; }

        public virtual InfoUser Friend { get; set; }
    }
}
