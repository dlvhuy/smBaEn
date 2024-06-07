using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.OpenApi.Models;
using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Helper.Interfaces;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;
using System.Diagnostics.Eventing.Reader;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SocialMedia.Hubs.ImplementHubs
{
    public class PostHub : Hub
    {   
        private readonly static Dictionary<int, string> _connectionMap = new Dictionary<int, string>();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IToken _token;
        private readonly IPost _post;
        private readonly ILikePost _likePost;
        private readonly IMapper _mapper;
        private readonly ICommentPost _commentPost;
        private readonly INotifications _notifications;
        public PostHub(INotifications notifications,ICommentPost commentPost,IMapper mapper,ILikePost likePost, IPost post, IHttpContextAccessor httpContextAccessor, IToken token)
        {
            _mapper = mapper;
            _likePost = likePost;
            _token = token;
            _post = post;
            _httpContextAccessor = httpContextAccessor;
            _commentPost = commentPost;
            _notifications = notifications;
        }

        [Authorize]
        public override Task OnConnectedAsync()
        {
            try
            {
                string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                int UserId = _token.getUserFromToken(token).IdUser;

                if (UserId != null && !_connectionMap.Any(user => user.Key == UserId)) _connectionMap.Add(UserId, Context.ConnectionId);
            }
            catch (Exception ex)
            {
                 Clients.Caller.SendAsync("onError", "OnDisconnected" + ex.Message);
            }

            return base.OnConnectedAsync();
        }

        [Authorize]
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                int UserId = _token.getUserFromToken(token).IdUser;

                if (UserId != null && _connectionMap.Any(user => user.Key == UserId)) _connectionMap.Remove(UserId);
            }
            catch (Exception ex)
            {
                 Clients.Caller.SendAsync("onError", "OnDisconnected" + ex.Message);
            }

            return base.OnDisconnectedAsync(exception);
        }

        [Authorize]
        public async Task UpdateLikePost(LikePostRequest likePostRequest)
        {
            try
            {
                // người gọi thì nhận được cả isLike và Totallike còn những người khác thì chỉ nhận được totalLilke thoi
                string[] token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Split(" ");
                int UserId = _token.getUserFromToken(token[1]).IdUser;

                bool isUserLikePost = _likePost.GetIsUserLikePost(likePostRequest.idPost, UserId);

                LikePostResponse likePostResponse;

                if (isUserLikePost) likePostResponse = _likePost.DeleteLikePost(likePostRequest.idPost,UserId);
                else
                {
                    LikePost likePost= _mapper.Map<LikePost>(likePostRequest);
                    likePost.IdUser = UserId;
                    likePostResponse = _likePost.AddLikePost(likePost);

                    // tạo thông báo likePost người like khác chủ post
                    int UserIdByPost = _post.GetPostInPost(likePost.IdPost).IdUser;
                    if(UserIdByPost != UserId)
                    {
                        NotificationRequest notificationRequest = new NotificationRequest(UserIdByPost, 1, UserId,likePost.IdPost);
                        NotificationResponse newNotification = _notifications.CreateNotification(notificationRequest);
                        if (_connectionMap.ContainsKey(UserIdByPost) && newNotification != null)
                        {
                            MainResponse mainResponseNotification = new MainResponse
                            {
                                Object = newNotification,
                                success = true
                            };

                            string ConnectionIdByUserIdPost = _connectionMap[UserIdByPost];
                            await Clients.Client(ConnectionIdByUserIdPost).SendAsync("ReceiveNotification", mainResponseNotification);
                        }
                    }
                }
                //Trả về
                MainResponse mainResponseCaller = new MainResponse
                {
                    Object = likePostResponse,
                    success = true,
                };

                MainResponse mainResponseOthers = new MainResponse
                {
                    Object = likePostResponse.TotalLikes,
                    success = true
                };

                await Clients.Caller.SendAsync("ReceiveMessageCaller", mainResponseCaller, likePostRequest.idPost, Context.ConnectionId); ;
                await Clients.Others.SendAsync("ReceiveMessageOthers", mainResponseOthers,likePostRequest.idPost, Context.ConnectionId);

            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("MessageError", "Error :", ex.Message);
            }

        }

        [Authorize]
        public async Task AddCommentInPost(CommentPostRequest commentPostRequest)
        {
            string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            int UserId = _token.getUserFromToken(token).IdUser;
            CommentPostResponse CommentPost = _commentPost.CreateCommentPost(commentPostRequest, UserId);


            // tạo thông báo khi addComment
            int UserIdByPost = _post.GetPostInPost(CommentPost.IdPost).IdUser;
            if (UserIdByPost != UserId)
            { 
                NotificationRequest notificationRequest = new NotificationRequest(UserIdByPost, 3, UserId, CommentPost.IdPost);
                NotificationResponse newNotification = _notifications.CreateNotification(notificationRequest);
                if (_connectionMap.ContainsKey(UserIdByPost))
                {
                    MainResponse mainResponseNotification = new MainResponse
                    {
                        Object = newNotification,
                        success = true
                    };
                    string ConnectionIdByUserIdPost = _connectionMap[UserIdByPost];
                    await Clients.User(ConnectionIdByUserIdPost).SendAsync("ReceiveNotificationForPostUser", mainResponseNotification);
                }
            
            }
            //Trả về
            MainResponse mainResponse = new MainResponse()
            {
                success = true,
                Object = CommentPost
            };

            await Clients.Group(commentPostRequest.IdPost.ToString()).SendAsync("ReceiveCommentPost", mainResponse);
        }
       
        public async Task TogleGroupCommentPost(GroupCommentPostRequest groupCommentPostRequest)
        {
            ///chưa kip reder lại nên isOpen ngược lại nên là nó bị ngược
            if (!groupCommentPostRequest.isOpen) { await AddUserInGroupCommentPost(groupCommentPostRequest.idPost); }
            else { await RemoveUserInGroupCommentPost(groupCommentPostRequest.idPost); }

         }

        private async Task AddUserInGroupCommentPost(int idPost)
        {
            var listCommentPostResponse = _commentPost.GetCommentsPostInPost(idPost);
            MainResponse mainResponse = new MainResponse
            {
                Object = listCommentPostResponse,
                success = true
            };
            await Groups.AddToGroupAsync(Context.ConnectionId, idPost.ToString());
            await Clients.Caller.SendAsync("ReceiveMessagePostCommentGroup", mainResponse);
        }

        private async Task RemoveUserInGroupCommentPost(int idPost)
        {
            MainResponse mainResponse = new MainResponse
            {
                Object = Array.Empty<PostContentResponse>,
                success = true
            };
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, idPost.ToString());
            await Clients.Caller.SendAsync("ReceiveMessagePostCommentGroup", $"{Context.ConnectionId} Rời group: {idPost}.");
        }
        
        
    }
}
