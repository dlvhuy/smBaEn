using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Models;

namespace SocialMedia.Repositories.Interfaces
{
    public interface IPost : IDisposable
    {
        IEnumerable<PostResponse> GetAllPostInUser(int  userId);

        IEnumerable<PostResponse> GetAllPostInGroup(int GroupId);

        IEnumerable<PostResponse> GetAllPosts();

        PostResponse GetPost(int PostId);

        PostResponse AddPost(int idUser,CreatePostRequest postRequest);

        bool UpdatePost (int id, CreatePostRequest postRequest);

        bool DeletePost(int id);

    }
}
