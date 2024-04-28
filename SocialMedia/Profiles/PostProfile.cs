using AutoMapper;
using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Models;

namespace SocialMedia.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<CreatePostRequest, Post>()
                 .ForMember(dest => dest.IdPost, src => src.MapFrom(x => x.Equals(null)))
                 .ForMember(dest => dest.IdGroup, src => src.MapFrom(x => x.IdGroup))
                 .ForMember(dest => dest.PostContent, src => src.MapFrom(x => x.PostContent));

            CreateMap<Post, PostResponse>()
                .ForMember(dest => dest.IdPost, src => src.MapFrom(x => x.IdPost))
                .ForMember(dest => dest.IdGroup, src => src.MapFrom(x => x.IdGroup))
                .ForMember(dest => dest.IdUser, src => src.MapFrom(x => x.IdUser))
                .ForMember(dest => dest.PostContent, src => src.MapFrom(x => x.PostContent));
        }
    }
}
