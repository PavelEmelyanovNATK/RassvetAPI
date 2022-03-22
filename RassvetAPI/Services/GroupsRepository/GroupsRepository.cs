using Microsoft.EntityFrameworkCore;
using RassvetAPI.Models.RassvetDBModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RassvetAPI.Services.GroupsRepository
{
    public class GroupsRepository : IGroupsRepository
    {
        private readonly RassvetDBContext _dao;

        public GroupsRepository(RassvetDBContext dao)
        {
            _dao = dao;
        }

        public Task AddGroup(SectionGroup group)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<SectionGroup>> GetAllGroups()
            => await _dao.SectionGroups.ToListAsync();

        public async Task<SectionGroup> GetGroup(int groupID)
            => await _dao.SectionGroups.FindAsync(groupID);

        public Task RemoveGroup(SectionGroup group)
        {
            throw new System.NotImplementedException();
        }
    }
}
