using SocialMedia.Models;
using SocialMedia.Services.PostService.Dtos.Request;
using SocialMedia.Services.PostService.Dtos.Response;

namespace SocialMedia.Repositories.Interfaces
{
    public interface IPostContent:IDisposable
    {
        List<PostContentResponse> AddPostContent(List<PostContentRequest> postContentRequest,int idPost);

        bool DeletePostContent(int idPostContent);

        IEnumerable<PostContentResponse> GetPostContentList(int idPost);
            
    }
}
