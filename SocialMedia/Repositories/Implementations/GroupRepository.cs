using AutoMapper;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;

namespace SocialMedia.Repositories.Implementations
{
    public class GroupRepository : IGroup
    {
        private readonly SociaMediaContext _dbontext;
        private readonly IMapper _mapper;
        public GroupRepository() { }
        public bool CreateGroup(Group group)
        {
            throw new NotImplementedException();
        }

        public bool DeleteGroup(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            try{ }
            catch { }
        }

        public IEnumerable<Group> GetAllGroups()
        {
            throw new NotImplementedException();
        }

        public Group GetGroupById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateGroup(Group group)
        {
            throw new NotImplementedException();
        }
    }
}
