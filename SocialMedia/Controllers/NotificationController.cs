using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Helper.Interfaces;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;


namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotifications _notification;
        private readonly IToken _token;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public NotificationController(IHttpContextAccessor httpContextAccessor, IToken token, INotifications notification)
        {
            _httpContextAccessor = httpContextAccessor;
            _notification = notification;
            _token = token;
        }


        [HttpGet]
        [Route("GetTotalNotificationsUnRead")]
        [Authorize]
        public IActionResult GetTotalNotificationsUnRead()
        {
            try
            {
                string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                int UserId = _token.getUserFromToken(token).IdUser;

                int TotalNotificationUnRead = _notification.TotalNotificationUnRead(UserId);
                return Ok(TotalNotificationUnRead);
            }
            catch (Exception ex) { return BadRequest(ex); }

        }

        [HttpGet]
        [Route("GetNotifications")]
        [Authorize]
        public IActionResult GetNotifications() {
            try
            {
                string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                int UserId = _token.getUserFromToken(token).IdUser;

                IEnumerable<NotificationResponse> listNotification = _notification.GetUserNotifications(UserId);
                return Ok(listNotification.Reverse());   
            }
            catch (Exception ex) { return BadRequest(ex); }

        }
    }
}
