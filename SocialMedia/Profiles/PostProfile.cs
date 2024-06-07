using AutoMapper;
using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Helper.AutoMapperHelper;
using SocialMedia.Models;
using SocialMedia.Repositories.Implementations;
using SocialMedia.Repositories.Interfaces;

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
                .ForMember(dest => dest.User, src => src.MapFrom<GetIDToUserInfo>())
                .ForMember(dest => dest.PostContent, src => src.MapFrom(x => x.PostContent))
                .ForMember(dest => dest.LikePost, src => src.MapFrom<GetIsUserLikePost>())
                .ForMember(dest => dest.postContentResponses, src => src.MapFrom<postContentToPostContentResponse>());
               
               
                
        }
    }
}
