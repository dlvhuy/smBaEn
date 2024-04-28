using SocialMedia.Models;

namespace SocialMedia.Repositories.Interfaces
{
    public interface ICommentPost : IDisposable
    {
        IEnumerable<CommentPost> GetAllCommentPost();
        CommentPost GetCommentPostById(int id);

        bool CreateCommentPost(CommentPost commentPost);

        bool UpdateCommentPost(int id ,CommentPost commentPost);

        bool DeleteCommentPost(int id);
    }
}
