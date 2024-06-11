using SocialMedia.Dtos.Requests;

namespace SocialMedia.Services.NotificationService
{
    public interface INotificationService : IDisposable 
    {
        void SendNotificationFriendRequest(int idUserToSend,NotificationFriendRequest request);
        void SendNotification(int idUserToSend,NotificationRequest request);

        Task DeletetNotificationFriendRequestHasSend (int idUserToSend,NotificationFriendRequest request);

    }
}
