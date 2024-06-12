using SocialMedia.Models;
using SocialMedia.Services.CommentService.Dtos.Request;
using SocialMedia.Services.CommentService.Dtos.Response;

namespace SocialMedia.Repositories.Interfaces
{
    public interface ICommentPost : IDisposable
    {
        IEnumerable<CommentPost> GetAllCommentPost();
        CommentPost GetCommentPostById(int id);

        CommentPostResponse CreateCommentPost(CommentPostRequest commentPostRequest,int IdUser);

        bool UpdateCommentPost(int id ,CommentPost commentPost);

        bool DeleteCommentPost(int id);

        IEnumerable<CommentPostResponse> GetCommentsPostInPost(int idPost);
    }
}
