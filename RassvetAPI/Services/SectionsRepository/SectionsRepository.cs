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
        private readonly RassvetDBContext _dao;

        public SectionsRepository(RassvetDBContext dao)
        {
            _dao = dao;
        }

        public async Task<List<Section>> GetAllSections()
        {
            return await _dao.Sections.ToListAsync();
        }

        public async Task<Section> GetSection(int ID)
        {
            return await _dao.Sections.FindAsync(ID);
        }
    }
}
