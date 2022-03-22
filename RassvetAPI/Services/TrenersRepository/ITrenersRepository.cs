using RassvetAPI.Models.RassvetDBModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RassvetAPI.Services.TrenersRepository
{
    public interface ITrenersRepository
    {
        Task<TrenerInfo> GetTrener(int trenerID);

        Task<List<TrenerInfo>> GetAllTreners();

        Task AddTrener(TrenerInfo trener);

        Task RemoveTrener(TrenerInfo trener);
    }
}
