using AutoMapper;
using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Helper.AutoMapperHelper;
using SocialMedia.Models;

namespace SocialMedia.Profiles
{
    public class InfoUserProfile : Profile
    {
        public InfoUserProfile()
        {
            CreateMap<SignUpRequest, InfoUser>()
                .ForMember(dest => dest.IdUser, src => src.MapFrom(x => x.Equals(null)))
                .ForMember(dest => dest.UserName, src => src.MapFrom(x => x.UserName))
                .ForMember(dest => dest.EmailUser, src => src.MapFrom(x => x.EmailUser))
                .ForMember(dest => dest.PasswordUser, src => src.MapFrom(x => x.PasswordUser))
                .ForMember(dest => dest.PhoneNumberUser, src => src.MapFrom(x => x.PhoneNumberUser))
                .ForMember(dest => dest.AvatarImage, src => src.MapFrom<GetDefaultAvatarImage>())
                .ForMember(dest => dest.CoverImage, src => src.MapFrom<GetDefaultCoverImage>())
                .ForMember(dest => dest.UserDescription, src => src.AllowNull());

           
            CreateMap<InfoUser,InfoUserResponse>()
                .ForMember(dest => dest.IdUser, src => src.MapFrom(x => x.IdUser))
                .ForMember(dest => dest.UserName, src => src.MapFrom(x => x.UserName))
                .ForMember(dest => dest.CoverImage, src => src.MapFrom<GetDefaultCoverImage>())
                .ForMember(dest => dest.AvatarImage, src => src.MapFrom<GetDefaultAvatarImage>())
                .ForMember(dest => dest.UserDescription, src => src.MapFrom(x => x.UserDescription));

            CreateMap<InfoUser, ItemSearchUser>()
               .ForMember(dest => dest.IdUser, src => src.MapFrom(x => x.IdUser))
               .ForMember(dest => dest.UserName, src => src.MapFrom(x => x.UserName))
               .ForMember(dest => dest.AvatarImage, src => src.MapFrom<GetDefaultAvatarImage>());
        }
    }
}
