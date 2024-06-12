using SocialMedia.Dtos.Respones;
using SocialMedia.Models;
using SocialMedia.Services.PostService.Dtos.Response;

namespace SocialMedia.Repositories.Interfaces
{
    public interface ILikePost : IDisposable
    {
        MainResponse GetTotalLikesInPost(int idPost);

        int GetTotalNumberLikesInPost(int postId);

        LikePostResponse AddLikePost(LikePost AddLikePost);

        LikePostResponse DeleteLikePost (int idPost,int idUser);

        bool GetIsUserLikePost(int idPost,int userId);
    }
}
