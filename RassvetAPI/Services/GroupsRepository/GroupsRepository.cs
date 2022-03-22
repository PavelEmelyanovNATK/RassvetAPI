using Microsoft.EntityFrameworkCore;
using RassvetAPI.Models.RassvetDBModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RassvetAPI.Services.GroupsRepository
{
    public class GroupsRepository : IGroupsRepository
    {
        public Task AddGroup(SectionGroup group)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<SectionGroup>> GetAllGroups()
        {
            using RassvetDBContext _dao = new RassvetDBContext();
            return await _dao.SectionGroups.ToListAsync();
        }

        public async Task<SectionGroup> GetGroup(int groupID)
        {
            using RassvetDBContext _dao = new RassvetDBContext();
            return await _dao.SectionGroups.FindAsync(groupID);
        }

        public Task RemoveGroup(SectionGroup group)
        {
            throw new System.NotImplementedException();
        }
    }
}
