using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;
using SocialMedia.Services.PostService.Dtos.Request;
using SocialMedia.Services.PostService.Dtos.Response;
using System.Collections.Immutable;

namespace SocialMedia.Repositories.Implementations
{
    public class PostRepository : IPost
    {
        private readonly SociaMediaContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPostContent _postContent;
        public PostRepository(IPostContent postContent,SociaMediaContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _postContent = postContent;
        }
        public PostResponse AddPost(int idUser, CreatePostRequest postRequest)
        {
            try
            {
                Post post = _mapper.Map<Post>(postRequest);
                post.IdUser = idUser;

                _dbContext.Posts.Add(post);
                _dbContext.SaveChanges();

                var postResponse = _mapper.Map<PostResponse>(post);

                return postResponse;

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

        public List<PostResponse> GetAllPostInUser(int userId)
        {

            var listPost = _dbContext.Posts
                .Include(post => post.PostImageContents)
                .Where(posts => posts.IdUser == userId)
                .ToList();

            var listPostResponse = _mapper.Map<List<PostResponse>>(listPost);

            return listPostResponse;
        }

        public List<PostResponse> GetAllPosts()
        {
            var listPost = _dbContext.Posts
                .Include(post => post.PostImageContents)
                 .ToList();

            var listPostResponse = _mapper.Map<List<PostResponse>>(listPost);
            return listPostResponse;
        }

        public PostResponse GetPost(int PostId)
        {
            var Post = _dbContext.Posts
                 .Include(post => post.PostImageContents)
                 .Where(posts => posts.IdPost == PostId).SingleOrDefault();

            PostResponse postResponse = _mapper.Map<PostResponse>(Post);
            return postResponse;
        }

        public Post GetPostInPost(int PostId)
        {
            var Post = _dbContext.Posts
                 .Include(post => post.PostImageContents)
                 .Where(posts => posts.IdPost == PostId).SingleOrDefault();

            return Post;
        }

        public bool UpdatePost(int id, CreatePostRequest postRequest)
        {
            throw new NotImplementedException();
        }

     
    }
}
