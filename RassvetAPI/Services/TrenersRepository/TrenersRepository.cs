using Microsoft.EntityFrameworkCore;
using RassvetAPI.Models.RassvetDBModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RassvetAPI.Services.TrenersRepository
{
    public class TrenersRepository : ITrenersRepository
    {
        private readonly RassvetDBContext _dao;

        public TrenersRepository(RassvetDBContext dao)
        {
            _dao = dao;
        }

        public Task AddTrenerAsync(TrenerInfo trener)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<TrenerInfo>> GetAllTrenersAsync()
            => await _dao.TrenerInfos.ToListAsync();

        public async Task<TrenerInfo> GetTrenerAsync(int trenerID)
            => await _dao.TrenerInfos.FindAsync(trenerID);

        public Task RemoveTrenerAsync(TrenerInfo trener)
        {
            throw new System.NotImplementedException();
        }
    }
}
