using AutoMapper;
using SocialMedia.Dtos.Respones;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;
using System.Collections.Immutable;
using System.Linq;

namespace SocialMedia.Repositories.Implementations
{
    public class InfoUserRepository : IInforUser
    {
        private readonly IMapper _mapper;
        private readonly SociaMediaContext _dbContext;
        private readonly IPost _post;
        private readonly IFriends _friend;

        public InfoUserRepository(IFriends friend, IPost post, IMapper mapper, SociaMediaContext dbContext) {
            _mapper = mapper;
            _dbContext = dbContext;
            _post = post;
            _friend = friend;
        }
        public bool CreateUser(InfoUser user)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            try { }
            catch { }
        }

        public IEnumerable<InfoUser> GetAllUser()
        {
            throw new NotImplementedException();
        }

        public InfoUserResponse GetInfomationInUser(int idUser, int LoginUserId)
        {
            InfoUser userInfo = GetUserById(idUser);

            if (idUser == LoginUserId)
            {
                IEnumerable<PostResponse> listPostResponse = _post.GetAllPostInUser(idUser);
                InfoUserResponse userResponse = _mapper.Map<InfoUserResponse>(userInfo);
                userResponse.PostResponses = listPostResponse;
                userResponse.isCurrentUser = true;
                return userResponse;
            }
            else
            {
                IEnumerable<PostResponse> listPostResponse = _post.GetAllPostInUser(idUser);
                InfoUserResponse userResponse = _mapper.Map<InfoUserResponse>(userInfo);
                userResponse.PostResponses = listPostResponse;
                userResponse.FriendStatus = _friend.GetFriendStatus(idUser, LoginUserId);
                userResponse.isCurrentUser = false;
                return userResponse;
            }
        }

        public InfoUser GetUserById(int id)
        {
            InfoUser user = _dbContext.InfoUsers.SingleOrDefault(user => user.IdUser == id);

            return user;
        }

        public IEnumerable<ItemSearchUser> SearchUser(string searchString,InfoUser CurrentUser)
        {

            IEnumerable<InfoUser> listUserInfo = _dbContext.InfoUsers.Where(inforUser => inforUser.UserName.Contains(searchString)).Take(10).ToImmutableArray().DefaultIfEmpty();
            
            IEnumerable<ItemSearchUser> itemSearchUsers = _mapper.Map<IEnumerable<ItemSearchUser>>(listUserInfo);
        
            
                return itemSearchUsers;

            
     
            

        }

        public bool UpdateUser(InfoUser user)
        {
            throw new NotImplementedException();
        }
    }
}
