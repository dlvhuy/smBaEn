using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Hubs;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;
using SocialMedia.Services.CommentService.Dtos.Request;
using SocialMedia.Services.CommentService.Dtos.Response;
using SocialMedia.Services.NotificationService;

namespace SocialMedia.Services.CommentService
{
    public class CommentPostService : ICommentPostService
    {
        private readonly ICommentPost _commentPost;
        private readonly PostHub _hub;
        private readonly INotificationService _notificationService;
        private readonly IPost _post;

        public CommentPostService(IPost post, ICommentPost commentPost,INotificationService notificationService,PostHub hub)
        {
            _commentPost = commentPost;
            _hub = hub;
            _notificationService = notificationService;
            _post = post;
        }

        public void AddCommentPost(int idUserAddPost,CommentPostRequest commentPostRequest)
        {
            var CommentPost = _commentPost.CreateCommentPost(commentPostRequest, idUserAddPost);
            int idPostUser = _post.GetPostInPost(CommentPost.IdPost).IdUser;
            if (idPostUser != idUserAddPost)
            {
                NotificationRequest notificationRequest = new NotificationRequest(idPostUser, 3, idUserAddPost, CommentPost.IdPost);
                _notificationService.SendNotification(idPostUser, notificationRequest);
            }
            _hub.SendCommentPostToPostGroup(CommentPost.IdPost, CommentPost);
        }

        public void Dispose()
        {
            try { }
            catch { }
        }

        public IEnumerable<CommentPostResponse> GetCommentInPost(int idPost)
        {
            var listCommentPost = _commentPost.GetCommentsPostInPost(idPost);
            return listCommentPost;
        }

        public int GetTotalCommentInPost(int idPost)
        {
            throw new NotImplementedException();
        }

        public void removeCommentPost()
        {
            throw new NotImplementedException();
        }

        public void UpdateCommentPost()
        {
            throw new NotImplementedException();
        }
    }
}
