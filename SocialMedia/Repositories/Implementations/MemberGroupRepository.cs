using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;

namespace SocialMedia.Repositories.Implementations
{
    public class MemberGroupRepository : IMemberGroup
    {
        public bool AddMemberIngroup(MemberGroup memberGroup)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMemberIngroup(MemberGroup memberGroup)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }               

        public IEnumerable<MemberGroup> GetAllMemberInGroup(int groupId)
        {
            throw new NotImplementedException();
        }

        public int GetTotalNumberMemberInGroup(int groupId)
        {
            throw new NotImplementedException();
        }
    }
}
