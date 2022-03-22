using Microsoft.EntityFrameworkCore;
using RassvetAPI.Models.RassvetDBModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RassvetAPI.Services.TrenersRepository
{
    public class TrenersRepository : ITrenersRepository
    {
        public Task AddTrener(TrenerInfo trener)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<TrenerInfo>> GetAllTreners()
        {
            using RassvetDBContext _dao = new RassvetDBContext();
            return await _dao.TrenerInfos.ToListAsync();
        }

        public async Task<TrenerInfo> GetTrener(int trenerID)
        {
            using RassvetDBContext _dao = new RassvetDBContext();
            return await _dao.TrenerInfos.FindAsync(trenerID);
        }

        public Task RemoveTrener(TrenerInfo trener)
        {
            throw new System.NotImplementedException();
        }
    }
}
