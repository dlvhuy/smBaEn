using SocialMedia.Services.CommentService.Dtos.Request;
using SocialMedia.Services.CommentService.Dtos.Response;

namespace SocialMedia.Services.CommentService
{
    public interface ICommentPostService : IDisposable
    {
        void AddCommentPost(int idUserAddPost, CommentPostRequest commentPostRequest);
        void removeCommentPost();
        void UpdateCommentPost();
        IEnumerable<CommentPostResponse> GetCommentInPost(int idPost);
        int GetTotalCommentInPost (int idPost);
    }
}
