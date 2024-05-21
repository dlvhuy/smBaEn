using SocialMedia.Dtos.Respones;
using SocialMedia.Models;

namespace SocialMedia.Repositories.Interfaces
{
    public interface IInforUser : IDisposable
    {
        IEnumerable<InfoUser> GetAllUser();

        InfoUser GetUserById(int id);

        bool CreateUser(InfoUser user);

        bool UpdateUser(InfoUser user);

        bool DeleteUser(int id);

        InfoUserResponse GetInfomationInUser(int idUser,int LoginUserId);

        IEnumerable<ItemSearchUser> SearchUser(string searchString, InfoUser CurrentUser);


    }
}
