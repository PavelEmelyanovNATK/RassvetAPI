using RassvetAPI.Models.RassvetDBModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RassvetAPI.Services.GroupsRepository
{
    public interface IGroupsRepository
    {
        Task<SectionGroup> GetGroup(int groupID);

        Task<List<SectionGroup>> GetAllGroups();

        Task AddGroup(SectionGroup group);

        Task RemoveGroup(SectionGroup group);
    }
}
