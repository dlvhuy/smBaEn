using SocialMedia.Models;
using System.Diagnostics.Eventing.Reader;

namespace SocialMedia.Repositories.Interfaces
{
    public interface IFriends : IDisposable
    {
        bool SendFriendRequest (int friendId);

        IEnumerable<Friends> GetUserFriends(int idUser);
        
        Friends GetFriend (int idFriend,int idUser);

        bool UpdateStatusFriendRequest (int idFriend,int idUser);

        bool RemoveFriend (int idFriend,int idUser);


    }
}
