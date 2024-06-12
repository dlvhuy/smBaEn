using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;

using System.Linq;

namespace SocialMedia.Repositories.Implementations
{
    public class NotificationRepository : INotifications
    {
        private readonly IMapper _mapper;
        private readonly SociaMediaContext _dbContext;
        public NotificationRepository(SociaMediaContext dbContext,IMapper mapper) {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public NotificationResponse CreateNotification(NotificationRequest notificationRequest)
        {
            //khi có những thuộc tính chung thì sẽ update thời gian
            Notifications newNotification = _mapper.Map<Notifications>(notificationRequest);
             
            _dbContext.Notifications.Add(newNotification);
            _dbContext.SaveChanges();

            return _mapper.Map<NotificationResponse>(newNotification);

        }

        public NotificationResponse CreateNotification(NotificationFriendRequest notificationFriendRequest)
        {
            Notifications newNotification = _mapper.Map<Notifications>(notificationFriendRequest);
           
            _dbContext.Notifications.Add(newNotification);
            _dbContext.SaveChanges();

            return _mapper.Map<NotificationResponse>(newNotification);
        }

        public NotificationResponse DeleteNotificationbyId(int idNotification)
        {
            throw new NotImplementedException();
        }

        public NotificationResponse DeleteNotificationbyPropeties(int idUser, string type, int idUserRelative, int idItemRelative)
        {
            if (!_dbContext.Notifications
                .Where(Noti => Noti.IdUser == idUser &&
                Noti.TypeNotification.Equals(type) &&
                Noti.IdUserRelative == idUserRelative &&
                Noti.IdItemRelative == idItemRelative
            ).Any()) return null;


            var deleteNotification = _dbContext.Notifications
                .Where(Noti => Noti.IdUser == idUser &&
                Noti.TypeNotification.Equals(type) &&
                Noti.IdUserRelative == idUserRelative &&
                Noti.IdItemRelative == idItemRelative
            ).SingleOrDefault();

            _dbContext.Notifications.Remove(deleteNotification);
            _dbContext.SaveChanges();

            return _mapper.Map<NotificationResponse>(deleteNotification);
        }
        public NotificationResponse DeleteNotificationbyPropeties(int idUser, string type, int idUserRelative)
        {
            if (!_dbContext.Notifications
                .Where(Noti => Noti.IdUser == idUser &&
                Noti.TypeNotification.Equals(type) &&
                Noti.IdUserRelative == idUserRelative
            ).Any()) return null;


            var deleteNotification = _dbContext.Notifications
                .Where(Noti => Noti.IdUser == idUser &&
                Noti.TypeNotification.Equals(type) &&
                Noti.IdUserRelative == idUserRelative 
            ).SingleOrDefault();

            _dbContext.Notifications.Remove(deleteNotification);
            _dbContext.SaveChanges();

            return _mapper.Map<NotificationResponse>(deleteNotification);
        }
        public IEnumerable<NotificationResponse> GetUserNotifications(int idUser)
        {
            IEnumerable<Notifications> listNotification = _dbContext.Notifications.Where(noti => noti.IdUser == idUser).ToList();
            return _mapper.Map<IEnumerable<NotificationResponse>>(listNotification);
        }

        public int TotalNotificationUnRead(int idUser)
        {
            return _dbContext.Notifications.Where(noti => noti.IdUser == idUser && noti.StatusNotification.Equals("unread")).Count();
        }

        public NotificationResponse UpdateStatusNotification(int idNotification)
        {
            throw new NotImplementedException();
        }

        
    }
}
