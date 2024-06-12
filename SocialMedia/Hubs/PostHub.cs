﻿using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.OpenApi.Models;
using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Helper.AutoMapperHelper;
using SocialMedia.Helper.Interfaces;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;
using SocialMedia.Services.CommentService.Dtos.Request;
using SocialMedia.Services.CommentService.Dtos.Response;
using SocialMedia.Services.PostService.Dtos.Response;
using System.Diagnostics.Eventing.Reader;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SocialMedia.Hubs
{
    public class PostHub : Hub
    {
        public readonly static Dictionary<int, string> _connectionMap = new Dictionary<int, string>();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IToken _token;
        private readonly IPost _post;
        private readonly ILikePost _likePost;
        private readonly IMapper _mapper;
        private readonly ICommentPost _commentPost;
        private readonly INotifications _notifications;
        private readonly IFriends _friends;
        private readonly IHubContext<PostHub> _hubContext;
        public PostHub(IHubContext<PostHub> hubContext, IFriends friends, INotifications notifications, ICommentPost commentPost, IMapper mapper, ILikePost likePost, IPost post, IHttpContextAccessor httpContextAccessor, IToken token)
        {
            _hubContext = hubContext;
            _mapper = mapper;
            _likePost = likePost;
            _token = token;
            _post = post;
            _httpContextAccessor = httpContextAccessor;
            _commentPost = commentPost;
            _notifications = notifications;
            _friends = friends;
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
            catch (Exception ex) { Clients.Caller.SendAsync("onError", "OnDisconnected" + ex.Message); }

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
            catch (Exception ex) { Clients.Caller.SendAsync("onError", "OnDisconnected" + ex.Message); }

            return base.OnDisconnectedAsync(exception);
        }


        public async Task UpdateLikePostTest(int idUserCaller, int idPost, LikePostResponse likePostResponse)
        {
            MainResponse mainResponseCaller = returnMainResponse(likePostResponse);
            MainResponse mainResponseOthers = returnMainResponse(likePostResponse.TotalLikes);

            string ConnectionIdByUserIdPost = _connectionMap[idUserCaller];
            await _hubContext.Clients.Client(ConnectionIdByUserIdPost).SendAsync("ReceiveMessageCaller", mainResponseCaller, idPost);
            await _hubContext.Clients.AllExcept(ConnectionIdByUserIdPost).SendAsync("ReceiveMessageOthers", mainResponseOthers, idPost);

        }

        public async Task SendNotification(int idUser, NotificationResponse newNotification)
        {
            if (_connectionMap.ContainsKey(idUser))
            {
                MainResponse mainResponseNotification = returnMainResponse(newNotification);
                string ConnectionIdByUserIdPost = _connectionMap[idUser];
                await _hubContext.Clients.Client(ConnectionIdByUserIdPost).SendAsync("ReceiveNotification", mainResponseNotification);
            }
        }
        public async Task SendCommentPostToPostGroup(int idPost, CommentPostResponse commentPostResponse)
        {
            MainResponse mainResponse = returnMainResponse(commentPostResponse);
            await _hubContext.Clients.Group(idPost.ToString()).SendAsync("ReceiveCommentPost", mainResponse);
        }
        public async Task TogleGroupCommentPost(GroupCommentPostRequest groupCommentPostRequest)
        {
            ///chưa kip reder lại nên isOpen ngược lại nên là nó bị ngược
            if (!groupCommentPostRequest.isOpen) { await AddUserInGroupCommentPost(groupCommentPostRequest.idPost); }
            else { await RemoveUserInGroupCommentPost(groupCommentPostRequest.idPost); }

        }
        private async Task AddUserInGroupCommentPost(int idPost)
        {
            await _hubContext.Groups.AddToGroupAsync(Context.ConnectionId, idPost.ToString());
        }
        private async Task RemoveUserInGroupCommentPost(int idPost)
        {
            await _hubContext.Groups.RemoveFromGroupAsync(Context.ConnectionId, idPost.ToString());
        }

        public async Task SendFriendRequest(int idFriend)
        {
            try
            {
                string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                int UserId = _token.getUserFromToken(token).IdUser;

                bool IsSendFriendRequestSuccess = _friends.SendFriendRequest(UserId, idFriend);
                if (IsSendFriendRequestSuccess)
                {
                    // 4 là friendRequest 
                    NotificationFriendRequest notificationRequest = new NotificationFriendRequest(idFriend, 4, UserId);
                    NotificationResponse newNotification = _notifications.CreateNotification(notificationRequest);

                    if (_connectionMap.ContainsKey(idFriend))
                    {
                        MainResponse mainResponseNotification = returnMainResponse(newNotification);
                        string ConnectionIdByUserIdPost = _connectionMap[idFriend];
                        await Clients.Client(ConnectionIdByUserIdPost).SendAsync("ReceiveNotification", mainResponseNotification);

                        var FriendResponseForFriend = _friends.GetFriend(UserId, idFriend);
                        MainResponse mainResponseFriendUpdateStateForFriend = returnMainResponse(FriendResponseForFriend);
                        await Clients.Client(ConnectionIdByUserIdPost).SendAsync("UpdateFriendState", mainResponseFriendUpdateStateForFriend);
                    }
                    var FriendResponse = _friends.GetFriend(idFriend, UserId);
                    MainResponse mainResponseFriendUpdateState = returnMainResponse(FriendResponse);
                    await Clients.Caller.SendAsync("UpdateFriendState", mainResponseFriendUpdateState);
                }
            }
            catch (Exception ex) { await Clients.Caller.SendAsync("MessageError", "Error :", ex.Message); }

        }
        // người nhận response là người gửi request == idUserResponse
        // người gửi response là người đã chấp nhận hoặc từ chới request là idUser bằng token


        [Authorize]
        public async Task ResponseFriendRequest(int idUserResponse, bool isAccept)
        {
            try
            {
                string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                int UserId = _token.getUserFromToken(token).IdUser;

                var deleteNotification = _notifications.DeleteNotificationbyPropeties(UserId, "friend_Request", idUserResponse);
                if (deleteNotification != null)
                {
                    MainResponse mainResponseNotification = returnMainResponse(deleteNotification);
                    await Clients.Caller.SendAsync("ReceiveDeleteNotification", mainResponseNotification);
                }

                if (isAccept) { await RequestFriendAccepted(UserId, idUserResponse); }
                else { await RequestFriendRejected(UserId, idUserResponse); }

                var FriendResponse1 = _friends.GetFriend(idUserResponse, UserId);
                MainResponse mainResponseFriendUpdateState1 = returnMainResponse(FriendResponse1);
                await Clients.Caller.SendAsync("UpdateFriendState", mainResponseFriendUpdateState1);

                if (_connectionMap.ContainsKey(idUserResponse))
                {
                    var FriendResponse2 = _friends.GetFriend(UserId, idUserResponse);
                    MainResponse mainResponseFriendUpdateState2 = returnMainResponse(FriendResponse2);
                    await Clients.Client(_connectionMap[idUserResponse]).SendAsync("UpdateFriendState", mainResponseFriendUpdateState2);
                }

            }
            catch (Exception ex) { await Clients.Caller.SendAsync("MessageError", "Error :", ex.Message); }
        }


        //cut
        private async Task RequestFriendAccepted(int idUser, int idFriend)
        {
            bool updateFriendSuccess = _friends.UpdateStatusFriendRequest(idFriend, idUser);
            if (updateFriendSuccess)
            {
                // 5 là Friend Accepted
                NotificationFriendRequest notificationRequest = new NotificationFriendRequest(idFriend, 5, idUser);
                NotificationResponse newNotification = _notifications.CreateNotification(notificationRequest);

                if (_connectionMap.ContainsKey(idFriend))
                {
                    MainResponse mainResponseNotification = returnMainResponse(newNotification);
                    string ConnectionIdByUserIdPost = _connectionMap[idFriend];
                    await Clients.Client(ConnectionIdByUserIdPost).SendAsync("ReceiveNotification", mainResponseNotification);
                }

            }
        }

        private async Task RequestFriendRejected(int idUser, int idFriend)
        {
            bool updateFriendSuccess = _friends.RemoveFriend(idFriend, idUser);
            if (updateFriendSuccess)
            {
                // 5 là Friend Rejected
                NotificationFriendRequest notificationRequest = new NotificationFriendRequest(idFriend, 6, idUser);
                NotificationResponse newNotification = _notifications.CreateNotification(notificationRequest);

                if (_connectionMap.ContainsKey(idFriend))
                {
                    MainResponse mainResponseNotification = returnMainResponse(newNotification);
                    string ConnectionIdByUserIdPost = _connectionMap[idFriend];
                    await Clients.Client(ConnectionIdByUserIdPost).SendAsync("ReceiveNotification", mainResponseNotification);
                }
            }
        }
        private MainResponse returnMainResponse(object reponse)
        {
            MainResponse mainResponse = new MainResponse
            {
                Object = reponse,
                success = true
            };

            return mainResponse;
        }
        public async Task Test(string message, int idUser)
        {
            string ConnectionIdByUserIdPost = _connectionMap[idUser];
            await Clients.Client(ConnectionIdByUserIdPost).SendAsync("ReceiveTest", message);
        }


    }
}