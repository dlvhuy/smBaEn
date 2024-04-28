using AutoMapper;
using SocialMedia.Models;

namespace SocialMedia.Dtos.Respones
{
    public class GetInfoUserResponse 
    {
        public string UserName { get; set; } = null!;
        public string? UserDescription { get; set; }
        public string EmailUser { get; set; } = null!;
        public string PasswordUser { get; set; } = null!;
        public string PhoneNumberUser { get; set; } = null!;
    }
}
