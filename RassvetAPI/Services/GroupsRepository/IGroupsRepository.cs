using RassvetAPI.Models.RassvetDBModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RassvetAPI.Services.GroupsRepository
{
    public interface IGroupsRepository
    {
        Task<SectionGroup> GetGroupAsync(int groupID);

        Task<List<SectionGroup>> GetAllGroupsAsync();

        Task AddGroupAsync(SectionGroup group);

        Task RemoveGroupAsync(SectionGroup group);
    }
}
