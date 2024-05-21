using AutoMapper;
using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Helper.Interfaces;
using SocialMedia.Models;

namespace SocialMedia.Helper.AutoMapperHelper
{
    public class GetDefaultCoverImage : IValueResolver<SignUpRequest, InfoUser, string>, IValueResolver<InfoUser, InfoUserResponse, string>
    {
        private readonly IManageImage _managerImage;
        public GetDefaultCoverImage(IManageImage managerImage) {
            _managerImage = managerImage;
        }
        public string Resolve(SignUpRequest source, InfoUser destination, string destMember, ResolutionContext context)
        {
            return _managerImage.GetDefaultCoverImage();
        }

        public string Resolve(InfoUser source, InfoUserResponse destination, string destMember, ResolutionContext context)
        {
            return _managerImage.SetDefaultCoverImage();
        }
    }
}
