using Microsoft.EntityFrameworkCore;
using RassvetAPI.Models.RassvetDBModels;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Services.RefreshTokensRepository
{
    public class RefreshTokensRepository : IRefreshTokensRepository
    {
        private readonly RassvetDBContext _dao;

        public RefreshTokensRepository(RassvetDBContext dao)
        {
            _dao = dao;
        }

        public async Task AddAsync(RefreshToken token)
        {
            _dao.RefreshTokens.Add(token);
            await _dao.SaveChangesAsync();
        }

        public async Task<RefreshToken> GetByIDAsync(int ID)
            => await _dao.RefreshTokens.FindAsync(ID);

        public async Task<RefreshToken> GetByTokenAsync(string token)
            => await _dao.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token); 

        public async Task<RefreshToken> GetByUserIDAsync(int UserID)
            => await _dao.RefreshTokens.FirstOrDefaultAsync(t => t.UserId == UserID);

        public async Task RemoveAsync(RefreshToken token)
        {
            var curToken = await GetByIDAsync(token.Id);

            if (curToken is null) return;

            _dao.RefreshTokens.Remove(curToken);
            await _dao.SaveChangesAsync();
        }

        public async Task RemoveAllAsync(int UserID)
        {
            var tokens = _dao.RefreshTokens.Where(t => t.UserId == UserID).AsEnumerable();

            if (tokens is null) return;

            _dao.RefreshTokens.RemoveRange(tokens);
            await _dao.SaveChangesAsync();
        }

        public async Task UpdateTokenAsync(int tokenId, string newToken)
        {
            var curToken = await GetByIDAsync(tokenId);
            curToken.Token = newToken;

            await _dao.SaveChangesAsync();
        }
    }
}
