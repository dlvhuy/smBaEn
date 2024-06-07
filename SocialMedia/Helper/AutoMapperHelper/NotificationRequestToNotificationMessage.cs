using AutoMapper;
using SocialMedia.Dtos.Requests;
using SocialMedia.Models;

namespace SocialMedia.Helper.AutoMapperHelper
{
    public class NotificationRequestToNotificationMessage : IValueResolver<NotificationRequest, Notifications, string>
    {
        public string Resolve(NotificationRequest source, Notifications destination, string destMember, ResolutionContext context)
        {
            switch (source.TypeNotification)
            {
                case 1:
                    return "Đã like bài viết của bạn";
                case 2:
                    return "Đã thêm bài viết";
                case 3:
                    return "Đã thêm comment vào bài viết của bạn";
                default:
                    return "other";
            }
        }
    }
}
