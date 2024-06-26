﻿using AutoMapper;
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
        public List<PostContentResponse> AddPostContent(List<PostContentRequest> postContentRequest,int idPost)
        {
            List<PostContent> listPostContent = _mapper.Map<List<PostContent>>(postContentRequest);
            foreach(PostContent  postContent in listPostContent)
            {
                postContent.IdPost = idPost;
                if(postContent.IdPost == null) { return null; }
                _dbContext.PostContents.Add(postContent);   
            }    
 
            _dbContext.SaveChanges();

            List<PostContentResponse> postContentResponses = _mapper.Map<List<PostContentResponse>>(listPostContent);

            return postContentResponses;

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
            try { }
            catch { }
            
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
