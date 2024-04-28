using SocialMedia.Models;

namespace SocialMedia.Repositories.Interfaces
{
    public interface IGroup : IDisposable
    {
        IEnumerable<Group> GetAllGroups();

        Group GetGroupById(int id);

        bool CreateGroup(Group group);

        bool UpdateGroup(Group group);

        bool DeleteGroup(int id);
    }
}
