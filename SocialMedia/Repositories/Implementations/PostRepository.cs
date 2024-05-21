using AutoMapper;
using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;
using System.Collections.Immutable;

namespace SocialMedia.Repositories.Implementations
{
    public class PostRepository : IPost
    {
        private readonly SociaMediaContext _dbContext;
        private readonly IMapper _mapper;
        public PostRepository(SociaMediaContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public PostResponse AddPost(int idUser, CreatePostRequest postRequest)
        {
            try
            {
                Post post = _mapper.Map<Post>(postRequest);
                post.IdUser = idUser;

                _dbContext.Posts.Add(post);
                _dbContext.SaveChanges();

                return _mapper.Map<PostResponse>(post);

            }catch(Exception ex)
            {
                return null;
            }
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



        public IEnumerable<PostResponse> GetAllPostInGroup(int GroupId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PostResponse> GetAllPostInUser(int userId)
        {

            IEnumerable<Post> listPost = _dbContext.Posts.Where(posts => posts.IdUser == userId).ToImmutableArray();

            IEnumerable<PostResponse> listPostResponse = _mapper.Map<IEnumerable<PostResponse>>(listPost);

            return listPostResponse;
        }

        public IEnumerable<PostResponse> GetAllPosts()
        {
            IEnumerable<Post> listPost = _dbContext.Posts.ToImmutableArray();

            IEnumerable<PostResponse> listPostResponse = _mapper.Map<IEnumerable<PostResponse>>(listPost);
            

            return listPostResponse;
        }

        public PostResponse GetPost(int PostId)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePost(int id, CreatePostRequest postRequest)
        {
            throw new NotImplementedException();
        }

     
    }
}
