using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;

namespace SocialMedia.Repositories.Implementations
{
    public class LikePostRepository : ILikePost
    {
        public bool AddLikePost(LikePost likePost)
        {
            throw new NotImplementedException();
        }

        public bool DeleteLikePost(LikePost likePost)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LikePost> GetTotalLikesInPost(int idPost)
        {
            throw new NotImplementedException();
        }

        public int GetTotalNumberLikesInPost(int postId)
        {
            throw new NotImplementedException();
        }
    }
}
