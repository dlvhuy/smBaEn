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
    }
}
