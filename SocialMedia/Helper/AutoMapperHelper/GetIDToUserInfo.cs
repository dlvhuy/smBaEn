
using AutoMapper;
using SocialMedia.Dtos.Respones;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;

using SocialMedia.Services.PostService.Dtos.Response;

namespace SocialMedia.Helper.AutoMapperHelper
{
    public class GetIDToUserInfo : IValueResolver<Post, PostResponse,ItemSearchUser> ,IValueResolver<CommentPost, CommentPostResponse, ItemSearchUser>,IValueResolver<Notifications,NotificationResponse,ItemSearchUser>
    {
        private readonly IInforUser _inforUser;
        private readonly IMapper _mapper;
        public GetIDToUserInfo(IMapper mapper, IInforUser inforUser)
        {
            _inforUser = inforUser;
            _mapper = mapper;
        }

        public ItemSearchUser Resolve(Post source, PostResponse destination, ItemSearchUser destMember, ResolutionContext context)
        {
            InfoUser userInfo = _inforUser.GetUserById(source.IdUser);
            ItemSearchUser itemSearchUser = _mapper.Map<ItemSearchUser>(userInfo);
            return itemSearchUser;
        }

        public ItemSearchUser Resolve(CommentPost source, CommentPostResponse destination, ItemSearchUser destMember, ResolutionContext context)
        {
            InfoUser userInfo = _inforUser.GetUserById(source.IdUser);
            ItemSearchUser itemSearchUser = _mapper.Map<ItemSearchUser>(userInfo);
            return itemSearchUser;
        }

        public ItemSearchUser Resolve(Notifications source, NotificationResponse destination, ItemSearchUser destMember, ResolutionContext context)
        {
            InfoUser userInfo = _inforUser.GetUserById(source.IdUserRelative);
            ItemSearchUser itemSearchUser = _mapper.Map<ItemSearchUser>(userInfo);
            return itemSearchUser;
        }
    }
}
