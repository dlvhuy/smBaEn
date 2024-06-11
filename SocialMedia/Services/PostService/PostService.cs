using Microsoft.AspNetCore.SignalR;
using SocialMedia.Dtos.Respones;
using SocialMedia.Hubs.ImplementHubs;
using SocialMedia.Repositories.Interfaces;
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
       
        
        public PostService(PostHub postHub, ILikePost likePost,IHubContext<PostHub> hubContext, IPostContent postContent, IPost post)
        {
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
