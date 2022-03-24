using RassvetAPI.Models.RassvetDBModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RassvetAPI.Services.SectionsRepository
{
    public interface ISectionsRepository
    {
        Task<Section> GetSectionAsync(int ID);
        Task<List<Section>> GetAllSectionsAsync();
        Task<List<Section>> GetClientSectionsAsync(int clientID);
    }
}
