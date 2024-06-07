using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Models;
using System.Diagnostics.Eventing.Reader;

namespace SocialMedia.Repositories.Interfaces
{
    public interface INotifications
    {
        NotificationResponse CreateNotification(NotificationRequest notificationRequest);

        NotificationResponse UpdateStatusNotification(int idNotification);

        IEnumerable<NotificationResponse> GetUserNotifications(int idUser);

        int TotalNotificationUnRead(int idUser);
    }
}
