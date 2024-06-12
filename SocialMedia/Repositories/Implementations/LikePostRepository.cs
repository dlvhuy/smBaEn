using Microsoft.Extensions.Hosting;
using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;
using SocialMedia.Services.PostService.Dtos.Response;

namespace SocialMedia.Repositories.Implementations
{
    public class LikePostRepository : ILikePost
    {
        private readonly SociaMediaContext _dbContext;

        
        public LikePostRepository(SociaMediaContext dbContext) {
            _dbContext = dbContext;
        }
        public LikePostResponse AddLikePost(LikePost AddLikePost)
        {
            try
            {
                _dbContext.LikePosts.Add(AddLikePost);
                _dbContext.SaveChanges();

                LikePostResponse likePostResponse = new LikePostResponse()
                {
                    isLike = GetIsUserLikePost(AddLikePost.IdPost, AddLikePost.IdUser),
                    TotalLikes = GetTotalNumberLikesInPost(AddLikePost.IdPost)
                };
                return likePostResponse; 

            }catch (Exception ex)
            {
                return null;
            }
        }

        public LikePostResponse DeleteLikePost(int idPost,int idUser)
        {
            try
            {
                LikePost likePostDelete = _dbContext.LikePosts.Where(likePost => likePost.IdPost == idPost && likePost.IdUser == idUser).FirstOrDefault();
                _dbContext.LikePosts.Remove(likePostDelete);
                _dbContext.SaveChanges();

                LikePostResponse likePostResponse = new LikePostResponse()
                {
                    isLike = GetIsUserLikePost(likePostDelete.IdPost, likePostDelete.IdUser),
                    TotalLikes = GetTotalNumberLikesInPost(likePostDelete.IdPost)
                };
                return likePostResponse;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void Dispose()
        {
            try { }
            catch { }
        }

        public bool GetIsUserLikePost(int idPost, int userId)
        {
            bool isUserLikePost = _dbContext.LikePosts.Where(post => post.IdPost ==  idPost && post.IdUser == userId).Any();
            return isUserLikePost;
        }

        public MainResponse GetTotalLikesInPost(int idPost)
        {
            
            throw new NotImplementedException();
        }

        public int GetTotalNumberLikesInPost(int postId)
        {
            int TotalLikesInPost = _dbContext.LikePosts.Where(post => post.IdPost == postId).Count();
            if(TotalLikesInPost > 0)
            {
                return TotalLikesInPost;
            }
            return 0;
            
        }
    }
}
