using AutoMapper;
using SocialMedia.Dtos.Requests;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;

namespace SocialMedia.Repositories.Implementations
{
    public class PostRepository : IPost
    {
        private readonly SociaMediaContext _dbContext;
        private readonly IMapper _mapper;
        public PostRepository(SociaMediaContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public bool AddPost(int idUser,CreatePostRequest postRequest)
        {

            Post post = _mapper.Map<Post>(postRequest);
            post.IdUser = idUser;

            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();
            
            return true;

        }

        public bool DeletePost(int id)
        {
            Post post = _dbContext.Posts.SingleOrDefault(post => post.IdPost == id);
            if (post == null) return false;

            _dbContext.Posts.Remove(post);
            _dbContext.SaveChanges();

            return true;
        }

        public void Dispose()
        {
            try { }
            catch { }
        }

        public IEnumerable<Post> GetAllPostInGroup(int GroupId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAllPostInUser(int userId)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePost(int id, CreatePostRequest postRequest)
        {
            throw new NotImplementedException();
        }
    }
}
