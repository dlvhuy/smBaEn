using AutoMapper;
using SocialMedia.Dtos.Respones;
using SocialMedia.Helper.Interfaces;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;
using SocialMedia.Services.PostService.Dtos.Response;

namespace SocialMedia.Helper.AutoMapperHelper
{
    public class GetIsUserLikePost : IValueResolver<Post, PostResponse, LikePostResponse>
    {
        private readonly ILikePost _likePost;
        

        public GetIsUserLikePost(ILikePost likePost) {
            _likePost = likePost;
            
        }
        public LikePostResponse Resolve(Post source, PostResponse destination, LikePostResponse destMember, ResolutionContext context)
        {
           
            int TotalLikes = _likePost.GetTotalNumberLikesInPost(source.IdPost);
            return new LikePostResponse()
            {
                isLike = false,
                TotalLikes = TotalLikes,
            };
            
        }
    }
}
