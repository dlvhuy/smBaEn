using AutoMapper;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;
using SocialMedia.Services.PostService.Dtos.Response;

namespace SocialMedia.Helper.AutoMapperHelper
{
    public class GetIDPostToTotalLike : IValueResolver<Post, PostResponse, int>
    {
        private readonly ILikePost _likePost;
        public GetIDPostToTotalLike(ILikePost likePost) {
            _likePost = likePost;
        }
        public int Resolve(Post source, PostResponse destination, int destMember, ResolutionContext context)
        {
            int TotalLikes = _likePost.GetTotalNumberLikesInPost(source.IdPost);
            return TotalLikes;
            
        }
    }
}
