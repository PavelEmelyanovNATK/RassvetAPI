using Microsoft.EntityFrameworkCore;
using RassvetAPI.Models.RassvetDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Services.SectionsRepository
{
    public class SectionsRepository : ISectionsRepository
    {
        public async Task<List<Section>> GetAllSections()
        {
            using RassvetDBContext _dao = new RassvetDBContext();
            return await _dao.Sections.ToListAsync(); 
        }

        public async Task<List<Section>> GetClientSections(int clientID)
        {
            using RassvetDBContext _dao = new RassvetDBContext();
            return await _dao.Sections
                .Where(s => s.Subscriptions
                .Any(sub => sub.ClientId == clientID))
                .ToListAsync();
        }

        public async Task<Section> GetSection(int ID)
        {
            using RassvetDBContext _dao = new RassvetDBContext();
            return await _dao.Sections.FindAsync(ID); 
        }
    }
}
