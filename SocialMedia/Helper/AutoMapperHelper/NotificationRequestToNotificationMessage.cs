using AutoMapper;
using SocialMedia.Dtos.Requests;
using SocialMedia.Models;

namespace SocialMedia.Helper.AutoMapperHelper
{
    public class NotificationRequestToNotificationMessage : IValueResolver<NotificationRequest, Notifications, string>,IValueResolver<NotificationFriendRequest, Notifications,string>
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
                case 4:
                    return "Đã gửi bạn lời mời kết bạn";
                case 5:
                    return "Đã chấp nhận lời kết bạn của bạn";
                case 6:
                    return "Đã từ chối lời kết bạn của bạn";
                default:
                    return "other";
            }
        }

        public string Resolve(NotificationFriendRequest source, Notifications destination, string destMember, ResolutionContext context)
        {
            switch (source.TypeNotification)
            {
                case 1:
                    return "Đã like bài viết của bạn";
                case 2:
                    return "Đã thêm bài viết";
                case 3:
                    return "Đã thêm comment vào bài viết của bạn";
                case 4:
                    return "Đã gửi bạn lời mời kết bạn";
                case 5:
                    return "Đã chấp nhận lời kết bạn của bạn";
                case 6:
                    return "Đã từ chối lời kết bạn của bạn";
                default:
                    return "other";
            }
        }
    }
}
