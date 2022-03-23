using Microsoft.EntityFrameworkCore;
using RassvetAPI.Models.RassvetDBModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Services.SectionsRepository
{
    public class SectionsRepository : ISectionsRepository
    {
        private readonly RassvetDBContext _dao;

        public SectionsRepository(RassvetDBContext dao)
        {
            _dao = dao;
        }

        public async Task<List<Section>> GetAllSections()
        {
            return await _dao.Sections.ToListAsync(); 
        }

        public async Task<List<Section>> GetClientSections(int clientID)
        {
            return await _dao.Sections
                .Where(s => s.Subscriptions
                .Any(sub => sub.ClientId == clientID))
                .ToListAsync();
        }

        public async Task<Section> GetSection(int ID)
        {
            return await _dao.Sections.FindAsync(ID); 
        }
    }
}
