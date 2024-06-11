using AutoMapper;
using SocialMedia.Dtos.Requests;
using SocialMedia.Models;

namespace SocialMedia.Profiles
{
    public class LikePostProfile : Profile
    {
        public LikePostProfile()
        {
            CreateMap<LikePostRequest, LikePost>()
            .ForMember(dest => dest.IdLikePost, src => src.MapFrom(x => x.Equals(null)))
            .ForMember(dest => dest.IdPost, src => src.MapFrom(x => x.idPost));

        }
    }
}
