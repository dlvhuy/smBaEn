using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;
using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Helper.AutoMapperHelper;
using SocialMedia.Models;

namespace SocialMedia.Profiles
{
    public class CommentPostProfile:Profile
    {
        public CommentPostProfile() {

            CreateMap<CommentPostRequest, CommentPost>()
                .ForMember(dest => dest.IdCommentPost, src => src.MapFrom(x => x.Equals(null)))
                .ForMember(dest => dest.IdPost, src => src.MapFrom(x => x.IdPost))
                .ForMember(dest => dest.ContentCommentPost, src => src.MapFrom(x => x.ContentCommentPost));

            CreateMap<CommentPost,CommentPostResponse>()
                .ForMember(dest => dest.IdCommentPost, src => src.MapFrom(x => x.IdCommentPost))
                .ForMember(dest => dest.IdPost, src => src.MapFrom(x => x.IdPost))
                .ForMember(dest => dest.User, src => src.MapFrom<GetIDToUserInfo>())
                .ForMember(dest => dest.ContentCommentPost, src => src.MapFrom(x => x.ContentCommentPost));

        }
    }
}
