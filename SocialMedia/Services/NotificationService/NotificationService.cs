using SocialMedia.Dtos.Requests;
using SocialMedia.Hubs;
using SocialMedia.Repositories.Implementations;
using SocialMedia.Repositories.Interfaces;

namespace SocialMedia.Services.NotificationService
{
    public class NotificationService : INotificationService
    {
        private readonly INotifications _notifications;
        private readonly PostHub _hub;

        public NotificationService(INotifications notifications,PostHub hub)
        {
            _notifications = notifications;
            _hub = hub;

        }
        public Task DeletetNotificationFriendRequestHasSend(int idUserToSend, NotificationFriendRequest request)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            try { }
            catch { }
            
        }

        public void SendNotification(int idUserToSend, NotificationRequest request)
        {
            var notificationResponse = _notifications.CreateNotification(request);
            _hub.SendNotification(idUserToSend, notificationResponse);
        }

        public void SendNotificationFriendRequest(int idUserToSend, NotificationFriendRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
