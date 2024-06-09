using AutoMapper;
using SocialMedia.Dtos.Respones;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;

namespace SocialMedia.Repositories.Implementations
{
    public class FriendRepository : IFriends
    {
        private readonly SociaMediaContext _dbContext;
        private readonly IMapper _mapper;
        public FriendRepository(IMapper mapper, SociaMediaContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Dispose()
        {
            try { }
            catch { }
        }

        public string GetFriendStatus(int idFriend, int idUser)
        {
            Friends friend = _dbContext.Friends.Where(f => f.IdFriend == idFriend && f.IdUser == idUser).SingleOrDefault();
            if(friend == null)
            {
                return null;
            }
            return friend.Status;
        }

        public FriendResponse GetFriend(int idFriend, int idUser)
        {
            Friends friend = _dbContext.Friends.Where(f => f.IdFriend == idFriend && f.IdUser == idUser).SingleOrDefault();
            if (friend == null)
            {
                return null;
            }
            return _mapper.Map<FriendResponse>(friend);
        }

        public IEnumerable<ItemSearchUser> GetUserFriends(int idUser)
        {
            var listFriendsUser = _dbContext.Friends.Where(f => f.IdUser == idUser)
                .Select(f => new {f.IdFriend})
                .ToList();

            var Friends = _mapper.Map<IEnumerable<ItemSearchUser>>(listFriendsUser);
            return Friends;
        }

        public bool RemoveFriend(int userId, int friendId)
        {
            try
            {
                Friends friend1 = _dbContext.Friends.Where(f => f.IdFriend == friendId && f.IdUser == userId).SingleOrDefault();
                Friends friend2 = _dbContext.Friends.Where(f => f.IdFriend == userId && f.IdUser == friendId).SingleOrDefault();

                _dbContext.Friends.Remove(friend1);
                _dbContext.Friends.Remove(friend2);

                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex) { return false; }
        }

        public bool SendFriendRequest(int userId,int friendId)
        {
            try
            {
                Friends friend1 = new Friends()
                {
                    IdUser = userId,
                    IdFriend = friendId,
                    Status = "Pending"
                };
                Friends friend2 = new Friends()
                {
                    IdUser = friendId,
                    IdFriend = userId,
                    Status = "Received_Request"
                };

                _dbContext.Friends.Add(friend1);
                _dbContext.Friends.Add(friend2);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateStatusFriendRequest(int userId, int friendId)
        {
            try
            {
                Friends friend1 = _dbContext.Friends.Where(f => f.IdFriend == friendId && f.IdUser == userId).SingleOrDefault();
                Friends friend2 = _dbContext.Friends.Where(f => f.IdFriend == userId && f.IdUser == friendId).SingleOrDefault();
                friend1.Status = "Accepted"; 
                friend2.Status = "Accepted"; 

                _dbContext.Friends.Update(friend1);
                _dbContext.Friends.Update(friend2);

                _dbContext.SaveChanges();
                return true;
            }
            catch { return false; }
        }
    }
}
