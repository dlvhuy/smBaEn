using AutoMapper;
using SocialMedia.Models;
using SocialMedia.Services.PostService.Dtos.Request;

namespace SocialMedia.Services.PostService.Map
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
