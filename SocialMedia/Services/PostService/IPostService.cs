using AutoMapper.Configuration.Conventions;
using SocialMedia.Dtos.Respones;
using SocialMedia.Services.PostService.Dtos.Request;
using SocialMedia.Services.PostService.Dtos.Response;

namespace SocialMedia.Services.PostService
{
    public interface IPostService : IDisposable
    {
        Task<MainResponse> AddPost(int idUserAdd, CreatePostRequest createPostRequest);
        MainResponse UpdatePost(int idUserUpdate, CreatePostRequest postRequest);
        void DeletePost(int idPost);
        MainResponse GetPost(int idUserCall, int idPost);

        MainResponse GetPosts(int idUserCall);

        IEnumerable<PostResponse> GetAllPostInUser(int idUser,int idUserCall);

        void SearchPostByContent(string Content);
        void SearchPostsByUser(int idUserPost);
        
        void UpdateLikePost(int idUserCall,int idPost);

    }
}
