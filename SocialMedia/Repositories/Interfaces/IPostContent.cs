using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Models;

namespace SocialMedia.Repositories.Interfaces
{
    public interface IPostContent:IDisposable
    {
        bool AddPostContent(PostContentRequest postContentRequest);

        bool DeletePostContent(int idPostContent);

        IEnumerable<PostContentResponse> GetPostContentList(int idPost);
            
    }
}
