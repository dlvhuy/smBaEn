using SocialMedia.Models;

namespace SocialMedia.Repositories.Interfaces
{
    public interface IMemberGroup: IDisposable
    {
        IEnumerable<MemberGroup> GetAllMemberInGroup(int groupId);

        int GetTotalNumberMemberInGroup(int groupId);

        bool AddMemberIngroup(MemberGroup memberGroup);

        bool DeleteMemberIngroup(MemberGroup memberGroup);

    }
}
