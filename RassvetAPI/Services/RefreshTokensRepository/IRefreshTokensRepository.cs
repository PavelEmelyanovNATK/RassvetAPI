using RassvetAPI.Models.RassvetDBModels;
using System.Threading.Tasks;

namespace RassvetAPI.Services.RefreshTokensRepository
{
    public interface IRefreshTokensRepository
    {
        Task AddAsync(RefreshToken token);
        Task<RefreshToken> GetByIDAsync(int ID);
        Task<RefreshToken> GetByUserIDAsync(int UserID);
        Task<RefreshToken> GetByTokenAsync(string token);
        Task RemoveAsync(RefreshToken token);
        Task RemoveAllAsync(int UserID);
        Task UpdateTokenAsync(int tokenID, string newToken);
    }
}
