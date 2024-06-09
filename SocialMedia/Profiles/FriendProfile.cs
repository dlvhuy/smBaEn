using AutoMapper;
using SocialMedia.Dtos.Respones;
using SocialMedia.Models;

namespace SocialMedia.Profiles
{
    public class FriendProfile :Profile
    {
        public FriendProfile()
        {
            CreateMap<Friends, FriendResponse>()
            .ForMember(dest => dest.IdFriend ,src => src.MapFrom(f => f.IdFriend))
            .ForMember(dest => dest.Status ,src => src.MapFrom(f => f.Status));
        }
    }
}
