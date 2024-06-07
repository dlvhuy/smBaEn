using AutoMapper;
using SocialMedia.Dtos.Requests;
using SocialMedia.Models;

namespace SocialMedia.Helper.AutoMapperHelper
{
    public class NotificationRequestToNotificationType : IValueResolver<NotificationRequest, Notifications, string>
    {
        public string Resolve(NotificationRequest source, Notifications destination, string destMember, ResolutionContext context)
        {
            switch(source.TypeNotification)
            {
                case 1:
                    return "like_Post";
                case 2:
                    return "add_Post";
                case 3:
                    return "add_Comment";
                default:
                    return "other";
            }    
            
        }
    }
}
