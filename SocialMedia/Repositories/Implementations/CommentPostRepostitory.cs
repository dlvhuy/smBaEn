using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;

namespace SocialMedia.Repositories.Implementations
{
    public class CommentPostRepostitory : ICommentPost
    {
        private readonly SociaMediaContext _dbContext;
        public CommentPostRepostitory(SociaMediaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CreateCommentPost(CommentPost commentPost)
        {
            if(commentPost == null)
            {
                return false;
            }
            _dbContext.CommentPosts.Add(commentPost);
            _dbContext.SaveChanges();
            return true;
        }

        public bool DeleteCommentPost(int id)
        {
            var deleteCommentPost = _dbContext.CommentPosts.SingleOrDefault(CMP => CMP.IdPost == id);
            if(deleteCommentPost == null)
            {
                return false;
            }
            _dbContext.CommentPosts.Remove(deleteCommentPost);
            _dbContext.SaveChanges();
            return true;
        }
        public CommentPost GetCommentPostById(int id)
        {
            if (id == null) return null;

            var getCommentPost = _dbContext.CommentPosts.SingleOrDefault(CMP => CMP .IdPost == id);
            if(getCommentPost == null)
            {
                return null;
            }

            return getCommentPost;
                        
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CommentPost> GetAllCommentPost()
        {
            throw new NotImplementedException();
        }


        public bool UpdateCommentPost(int id, CommentPost commentPost)
        {
            throw new NotImplementedException();
        }
    }
}
