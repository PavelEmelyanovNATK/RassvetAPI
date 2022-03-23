using RassvetAPI.Models.RassvetDBModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RassvetAPI.Services.SectionsRepository
{
    public interface ISectionsRepository
    {
        Task<Section> GetSection(int ID);
        Task<List<Section>> GetAllSections();
        Task<List<Section>> GetClientSections(int clientID);
    }
}
