using RassvetAPI.Models.RassvetDBModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RassvetAPI.Services.TrenersRepository
{
    public interface ITrenersRepository
    {
        Task<TrenerInfo> GetTrenerAsync(int trenerID);

        Task<List<TrenerInfo>> GetAllTrenersAsync();

        Task AddTrenerAsync(TrenerInfo trener);

        Task RemoveTrenerAsync(TrenerInfo trener);
    }
}
