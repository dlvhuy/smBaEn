using AutoMapper;
using SocialMedia.Dtos.Respones;
using SocialMedia.Models;
using SocialMedia.Services.PostService.Dtos.Response;

namespace SocialMedia.Helper.AutoMapperHelper
{
    public class postContentToPostContentResponse : IValueResolver<Post, PostResponse, List<PostContentResponse>>
    {
        private readonly IMapper _mapper;
        public postContentToPostContentResponse(IMapper mapper)
        {

            _mapper = mapper;

        }
        public List<PostContentResponse> Resolve(Post source, PostResponse destination, List<PostContentResponse> destMember, ResolutionContext context)
        {
            List<PostContentResponse> postContentResponse = _mapper.Map<List<PostContentResponse>>(source.PostImageContents);
            return postContentResponse;
        }
    }
}
