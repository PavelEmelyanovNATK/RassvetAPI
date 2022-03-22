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

        public Task AddTrener(TrenerInfo trener)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<TrenerInfo>> GetAllTreners()
            => await _dao.TrenerInfos.ToListAsync();

        public async Task<TrenerInfo> GetTrener(int trenerID)
            => await _dao.TrenerInfos.FindAsync(trenerID);

        public Task RemoveTrener(TrenerInfo trener)
        {
            throw new System.NotImplementedException();
        }
    }
}
