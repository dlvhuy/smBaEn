using AutoMapper;
using SocialMedia.Helper.Interfaces;
using SocialMedia.Models;
using SocialMedia.Services.PostService.Dtos.Request;
using SocialMedia.Services.PostService.Dtos.Response;

namespace SocialMedia.Helper.AutoMapperHelper
{
    public class GetImagePostContent : IValueResolver<PostContentRequest, PostContent, string>, IValueResolver<PostContent, PostContentResponse, string>
    {
        private readonly IManageImage _manageImage;
        public GetImagePostContent(IManageImage manageImage)
        {
            _manageImage = manageImage;

        }
        public string Resolve(PostContentRequest source, PostContent destination, string destMember, ResolutionContext context)
        {
            return _manageImage.SaveImage64Base(source);
        }

        public string Resolve(PostContent source, PostContentResponse destination, string destMember, ResolutionContext context)
        {
            return _manageImage.ChangeNameFileToURL(source.UrlimageVideo);
        }
    }
}
