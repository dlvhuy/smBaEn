using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Hubs;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;
using SocialMedia.Services.NotificationService;
using SocialMedia.Services.PostService.Dtos.Request;
using SocialMedia.Services.PostService.Dtos.Response;

namespace SocialMedia.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly IPost _post;
        private readonly IPostContent _postContent;
        private readonly IHubContext<PostHub> _hubContext;
        private readonly ILikePost _likePost;
        private readonly PostHub _postHub;
        private readonly INotificationService _notificationService;



        public PostService(INotificationService notificationService ,PostHub postHub, ILikePost likePost,IHubContext<PostHub> hubContext, IPostContent postContent, IPost post)
        {
            _notificationService = notificationService;
            _post = post;
            _postContent = postContent;
            _hubContext = hubContext;
            _likePost = likePost;
            _postHub = postHub;
        }
        public MainResponse GetPosts(int idUserCall)
        {
            var postResponse = _post.GetAllPosts();
            postResponse.ForEach(post => post.LikePost.isLike = _likePost.GetIsUserLikePost(post.IdPost, idUserCall));

            MainResponse mainResponse = new MainResponse(postResponse,true);
            return mainResponse;
        }
        public MainResponse GetPost(int idUserCall,int idPost)
        {
            var postResponse = _post.GetPost(idPost);
            postResponse.LikePost.isLike = _likePost.GetIsUserLikePost(idPost, idUserCall);

            MainResponse mainResponse = new MainResponse(postResponse, true);
            
            return mainResponse;
        }
        public async Task<MainResponse> AddPost(int idUserAdd, CreatePostRequest createPostRequest)
        {
           var postResponse = _post.AddPost(idUserAdd, createPostRequest);
           postResponse.postContentResponses = _postContent.AddPostContent(createPostRequest.PostContentRequests,postResponse.IdPost);

            if (postResponse != null)
            {
                MainResponse mainResponse = new MainResponse(postResponse, true);
                await _hubContext.Clients.All.SendAsync("ReceiveMessagePost", mainResponse);
                return mainResponse;
            }
            else
            {
                MainResponse mainResponse = new MainResponse(null, false);
                return mainResponse;
            }
        }
        public IEnumerable<PostResponse> GetAllPostInUser(int idUser,int idUserCall)
        {
            var postsResponse = _post.GetAllPostInUser(idUser);
            postsResponse.ForEach(post => post.LikePost.isLike = _likePost.GetIsUserLikePost(post.IdPost, idUserCall));
            return postsResponse;
        }

        public void UpdateLikePost(int idUserCall, int idPost)
        {
            bool isUserLikePost = _likePost.GetIsUserLikePost(idPost, idUserCall);

            LikePostResponse likePostResponse;
            if (isUserLikePost) likePostResponse = _likePost.DeleteLikePost(idPost, idUserCall);
            else
            {
                LikePost likePost = new LikePost() { IdPost = idPost, IdUser = idUserCall };
                likePostResponse = _likePost.AddLikePost(likePost);

                var idPostUser = _post.GetPostInPost(idPost).IdUser;
                if(idPostUser != idUserCall)
                {
                    var notificationRequest = new NotificationRequest(idPostUser, 1, idUserCall, idPost);
                    _notificationService.SendNotification(idPostUser, notificationRequest);
                }
            }
            _postHub.UpdateLikePostTest(idUserCall,idPost, likePostResponse);
        }
        public void DeletePost(int idPost)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            try { }
            catch { }
        }


        public void SearchPostByContent(string Content)
        {
            throw new NotImplementedException();
        }

        public void SearchPostsByUser(int idUserPost)
        {
            throw new NotImplementedException();
        }

        public MainResponse UpdatePost(int idUserUpdate, CreatePostRequest postRequest)
        {
            throw new NotImplementedException();
        }

    }
}
