using AutoMapper;
using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Helper.Interfaces;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;
using System.Collections.Immutable;

namespace SocialMedia.Repositories.Implementations
{
    public class PostContentRepository : IPostContent
    {
        private readonly IMapper _mapper;
        private readonly SociaMediaContext _dbContext;
        private readonly IManageImage _image;

        public PostContentRepository(IMapper mapper,SociaMediaContext dbContext, IManageImage image) {
            _mapper = mapper;
            _dbContext = dbContext;
            _image = image;

        }
        public bool AddPostContent(PostContentRequest postContentRequest)
        {
            PostContent postContent = _mapper.Map<PostContent>(postContentRequest);

            if (postContent == null) return false;
            
            _dbContext.PostContents.Add(postContent);   
            _dbContext.SaveChanges();   

            return true;

        }

        public bool DeletePostContent(int idPostContent)
        {
              
            PostContent postContent = _dbContext.PostContents.SingleOrDefault(postContent => postContent.IdPostContent == idPostContent);

            if (postContent == null) return false; 
            
            _dbContext.PostContents.Remove(postContent);
            return true;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PostContentResponse> GetPostContentList(int idPos)
        {
            IEnumerable<PostContent> listPostContent = _dbContext.PostContents.Where(idPost => idPost
            .IdPostContent == idPos).ToImmutableArray();

            IEnumerable<PostContentResponse> listPostContenResponse = _mapper.Map<IEnumerable<PostContentResponse>>(listPostContent);

            return listPostContenResponse;

            
        }

        
    }
}
