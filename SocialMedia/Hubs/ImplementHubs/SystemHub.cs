using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SocialMedia.Helper.Interfaces;
using SocialMedia.Repositories.Interfaces;

namespace SocialMedia.Hubs.ImplementHubs
{
    public class SystemHub:Hub
    {
        private readonly Dictionary<int, string> _connectionMap = new Dictionary<int, string>();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IToken _token;
        
        public SystemHub( IHttpContextAccessor httpContextAccessor, IToken token)
        {
            _token = token;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public override Task OnConnectedAsync()
        {
            try
            {
                string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                int UserId = _token.getUserFromToken(token).IdUser;

                if (UserId != null && !_connectionMap.Any(user => user.Key == UserId))
                {
                    _connectionMap.Add(UserId, Context.ConnectionId);
                }
            }
            catch (Exception ex)
            {
                Clients.Caller.SendAsync("onError", "OnDisconnected" + ex.Message);
            }

             return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                int UserId = _token.getUserFromToken(token).IdUser;

                if (UserId != null && _connectionMap.Any(user => user.Key == UserId))
                {
                    _connectionMap.Remove(UserId);
                }

            }
            catch (Exception ex)
            {
                Clients.Caller.SendAsync("onError", "OnDisconnected" + ex.Message);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
