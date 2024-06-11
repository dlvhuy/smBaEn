

using AutoMapper;
using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Helper.AutoMapperHelper;
using SocialMedia.Models;


namespace SocialMedia.Services.NotificationService.Map
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<NotificationRequest, Notifications>()
                .ForMember(dest => dest.IdNotification, src => src.MapFrom(x => x.Equals(null)))
                .ForMember(dest => dest.IdUser, src => src.MapFrom(x => x.IdUser))
                .ForMember(dest => dest.MessageNotification, src => src.MapFrom<NotificationRequestToNotificationMessage>())
                .ForMember(dest => dest.TypeNotification, src => src.MapFrom<NotificationRequestToNotificationType>())
                .ForMember(dest => dest.StatusNotification, src => src.MapFrom(x => "unRead"))
                .ForMember(dest => dest.IdUserRelative, src => src.MapFrom(x => x.IdUserRelative))
                .ForMember(dest => dest.IdItemRelative, src => src.MapFrom(x => x.IdItemRelative));

            CreateMap<NotificationFriendRequest, Notifications>()
               .ForMember(dest => dest.IdNotification, src => src.MapFrom(x => x.Equals(null)))
               .ForMember(dest => dest.IdUser, src => src.MapFrom(x => x.IdUser))
               .ForMember(dest => dest.MessageNotification, src => src.MapFrom<NotificationRequestToNotificationMessage>())
               .ForMember(dest => dest.TypeNotification, src => src.MapFrom<NotificationRequestToNotificationType>())
               .ForMember(dest => dest.StatusNotification, src => src.MapFrom(x => "unRead"))
               .ForMember(dest => dest.IdUserRelative, src => src.MapFrom(x => x.IdUserRelative))
               .ForMember(dest => dest.IdItemRelative, src => src.AllowNull());

            CreateMap<Notifications, NotificationResponse>()
                .ForMember(dest => dest.IdNotification, src => src.MapFrom(x => x.IdNotification))
                .ForMember(dest => dest.UserInfo, src => src.MapFrom<GetIDToUserInfo>())
                .ForMember(dest => dest.TypeNotification, src => src.MapFrom(x => x.TypeNotification))
                .ForMember(dest => dest.MessageNotification, src => src.MapFrom(x => x.MessageNotification))
                .ForMember(dest => dest.IdItemRelative, src => src.MapFrom(x => x.IdItemRelative));


        }
    }
}
