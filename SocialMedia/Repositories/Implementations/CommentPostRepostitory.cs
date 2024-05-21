using AutoMapper;
using AutoMapper.Configuration.Conventions;
using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;
using System.Collections.Immutable;

namespace SocialMedia.Repositories.Implementations
{
    public class CommentPostRepostitory : ICommentPost
    {
        private readonly SociaMediaContext _dbContext;
        private readonly IMapper _mapper;
        public CommentPostRepostitory(SociaMediaContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public CommentPostResponse CreateCommentPost(CommentPostRequest commentPostRequest,int IdUser)
        {
            try { 
                CommentPost commentPost = _mapper.Map<CommentPost>(commentPostRequest);
                commentPost.IdUser = IdUser;
                if(commentPost == null)
                {
                    return null;
                }
                _dbContext.CommentPosts.Add(commentPost);
                _dbContext.SaveChanges();

            
            
                return _mapper.Map<CommentPostResponse>(commentPost);
            }
            catch (Exception ex) { return null; }
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
            try { }
            catch { }
        }

        public IEnumerable<CommentPost> GetAllCommentPost()
        {
            throw new NotImplementedException();
        }


        public bool UpdateCommentPost(int id, CommentPost commentPost)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CommentPostResponse> GetCommentsPostInPost(int idPost)
        {
           IEnumerable<CommentPost> listCommentPostInPost = _dbContext.CommentPosts.Where(commentPost => commentPost.IdPost == idPost).ToImmutableArray();

            IEnumerable<CommentPostResponse> listCommentPostResponseInPost = _mapper.Map<IEnumerable<CommentPostResponse>>(listCommentPostInPost);
            return listCommentPostResponseInPost;
        }

        
    }
}
