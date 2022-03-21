using Microsoft.EntityFrameworkCore;
using RassvetAPI.Models.RassvetDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Services.RefreshTokensRepository
{
    public class RefreshTokensRepository : IRefreshTokensRepository
    {
        public async Task Add(RefreshToken token)
        {
            using RassvetDBContext _dao = new RassvetDBContext();
            _dao.RefreshTokens.Add(token);
            await _dao.SaveChangesAsync();
        }

        public async Task<RefreshToken> GetByID(int ID)
        {
            using RassvetDBContext _dao = new RassvetDBContext();
            return await _dao.RefreshTokens.FindAsync(ID);
        }

        public async Task<RefreshToken> GetByToken(string token)
        {
            using RassvetDBContext _dao = new RassvetDBContext();
            return await _dao.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token); 
        }

        public async Task<RefreshToken> GetByUserID(int UserID)
        {
            using RassvetDBContext _dao = new RassvetDBContext();
            return await _dao.RefreshTokens.FirstOrDefaultAsync(t => t.UserId == UserID);
        }
        

        public async Task Remove(RefreshToken token)
        {
            using RassvetDBContext _dao = new RassvetDBContext();
            var curToken = await GetByID(token.Id);

            if (curToken is null) return;

            _dao.RefreshTokens.Remove(curToken);
            await _dao.SaveChangesAsync();
        }

        public async Task RemoveAll(int UserID)
        {
            using RassvetDBContext _dao = new RassvetDBContext();
            var tokens = _dao.RefreshTokens.Where(t => t.UserId == UserID).AsEnumerable();

            if (tokens is null) return;

            _dao.RefreshTokens.RemoveRange(tokens);
            await _dao.SaveChangesAsync();
        }

        public async Task UpdateToken(RefreshToken token, string newToken)
        {
            using RassvetDBContext _dao = new RassvetDBContext();
            var curToken = await GetByID(token.Id);
            curToken.Token = newToken;
            await _dao.SaveChangesAsync();
        }
    }
}
