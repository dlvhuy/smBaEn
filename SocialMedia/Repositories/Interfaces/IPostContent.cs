using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Models;

namespace SocialMedia.Repositories.Interfaces
{
    public interface IPostContent:IDisposable
    {
        List<PostContentResponse> AddPostContent(List<PostContentRequest> postContentRequest,int idPost);

        bool DeletePostContent(int idPostContent);

        IEnumerable<PostContentResponse> GetPostContentList(int idPost);
            
    }
}
