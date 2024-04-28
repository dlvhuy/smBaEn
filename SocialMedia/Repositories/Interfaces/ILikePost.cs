using SocialMedia.Models;

namespace SocialMedia.Repositories.Interfaces
{
    public interface ILikePost : IDisposable
    {
        IEnumerable<LikePost> GetTotalLikesInPost(int idPost);

        int GetTotalNumberLikesInPost(int  postId);

        bool AddLikePost(LikePost likePost);

        bool DeleteLikePost (LikePost likePost);
    }
}
