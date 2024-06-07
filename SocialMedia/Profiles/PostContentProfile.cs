using AutoMapper;
using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Helper.AutoMapperHelper;
using SocialMedia.Helper.Interfaces;
using SocialMedia.Models;

namespace SocialMedia.Profiles
{
    public class PostContentProfile : Profile
    {
        private readonly IManageImage _image;
        public PostContentProfile(IManageImage image)
        {
            _image = image;
        }

        public PostContentProfile()
        {
            CreateMap<PostContentRequest, PostContent>()
                .ForMember(dest => dest.IdPostContent, src => src.MapFrom(x => x.Equals(null)))
                .ForMember(dest => dest.UrlimageVideo, src => src.MapFrom<GetImagePostContent>());

            CreateMap<PostContent, PostContentResponse>()
                .ForMember(dest => dest.IdPostContent, src => src.MapFrom(x => x.IdPostContent))
                .ForMember(dest => dest.UrlimageVideo, src => src.MapFrom<GetImagePostContent>());
 
        }
    }
}
