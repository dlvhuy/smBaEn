using AutoMapper;
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
            Notifications newNotification = _mapper.Map<Notifications>(notificationRequest);
            if (_dbContext.Notifications.Any(x => x.IdItemRelative == newNotification.IdItemRelative && x.IdUserRelative == newNotification.IdItemRelative))
                return null;

            _dbContext.Notifications.Add(newNotification);
            _dbContext.SaveChanges();

            return _mapper.Map<NotificationResponse>(newNotification);

        }

        public IEnumerable<NotificationResponse> GetUserNotifications(int idUser)
        {
            IEnumerable<Notifications> listNotification = _dbContext.Notifications.Where(noti => noti.IdUser == idUser).ToList();
            return _mapper.Map<IEnumerable<NotificationResponse>>(listNotification);
        }

        public int TotalNotificationUnRead(int idUser)
        {
            return _dbContext.Notifications.Where(noti => noti.IdUser == idUser && noti.StatusNotification == "unRead").Count();
        }

        public NotificationResponse UpdateStatusNotification(int idNotification)
        {
            throw new NotImplementedException();
        }
    }
}
