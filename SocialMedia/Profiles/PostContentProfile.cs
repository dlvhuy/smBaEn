using AutoMapper;
using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
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
                .ForMember(dest => dest.IdPostContent, src => src.AllowNull())
                .ForMember(dest => dest.IdPost, src => src.MapFrom(x => x.IdPost))
                .ForMember(dest => dest.UrlimageVideo, src => src.MapFrom(x => _image.SaveImage(x.UrlimageVideo)));


            CreateMap<PostContent, PostContentResponse>()
                .ForMember(dest => dest.IdPostContent, src => src.MapFrom(x => x.IdPostContent))
                .ForMember(dest => dest.UrlimageVideo, src => src.MapFrom(x => _image.ChangeNameFileToURL(x.UrlimageVideo)));
 
        }
    }
}
