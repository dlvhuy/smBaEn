using SocialMedia.Dtos.Requests;
using SocialMedia.Models;

namespace SocialMedia.Repositories.Interfaces
{
    public interface IPost : IDisposable
    {
        IEnumerable<Post> GetAllPostInUser(int  userId);

        IEnumerable<Post> GetAllPostInGroup(int GroupId);

        bool AddPost(int idUser,CreatePostRequest postRequest);

        bool UpdatePost (int id, CreatePostRequest postRequest);

        bool DeletePost(int id);

    }
}
