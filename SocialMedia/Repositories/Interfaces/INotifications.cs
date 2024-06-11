using AutoMapper.Configuration.Conventions;
using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Models;

using System.Diagnostics.Eventing.Reader;

namespace SocialMedia.Repositories.Interfaces
{
    public interface INotifications
    {
        NotificationResponse CreateNotification(NotificationRequest notificationRequest);

        NotificationResponse CreateNotification(NotificationFriendRequest notificationFriendRequest);

        NotificationResponse UpdateStatusNotification(int idNotification);

        IEnumerable<NotificationResponse> GetUserNotifications(int idUser);

        int TotalNotificationUnRead(int idUser);

        NotificationResponse DeleteNotificationbyPropeties(int idUser, string type, int idUserRelative, int idItemRelative);

        NotificationResponse DeleteNotificationbyPropeties(int idUser, string type, int idUserRelative);
        NotificationResponse DeleteNotificationbyId(int idNotification);
    }
}
