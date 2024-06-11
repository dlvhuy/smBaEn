using SocialMedia.Models;
using SocialMedia.Services.PostService.Dtos.Request;
using SocialMedia.Services.PostService.Dtos.Response;

namespace SocialMedia.Repositories.Interfaces
{
    public interface IPost : IDisposable
    {
        List<PostResponse> GetAllPostInUser(int userId);

        IEnumerable<PostResponse> GetAllPostInGroup(int GroupId);

        List<PostResponse> GetAllPosts();

        PostResponse GetPost(int PostId);

        Post GetPostInPost(int PostId);

        PostResponse AddPost(int idUser,CreatePostRequest postRequest);

        bool UpdatePost (int id, CreatePostRequest postRequest);

        bool DeletePost(int id);

    }
}
