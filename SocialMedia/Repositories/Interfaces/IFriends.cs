using SocialMedia.Dtos.Respones;
using SocialMedia.Models;
using System.Diagnostics.Eventing.Reader;

namespace SocialMedia.Repositories.Interfaces
{
    public interface IFriends : IDisposable
    {
        bool SendFriendRequest (int userId,int friendId);

        IEnumerable<ItemSearchUser> GetUserFriends(int idUser);

        string GetFriendStatus (int idFriend,int idUser);

        FriendResponse GetFriend(int idFriend, int idUser);

        bool UpdateStatusFriendRequest (int idFriend,int idUser);

        bool RemoveFriend (int userId, int friendId);


    }
}
