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
        private readonly RassvetDBContext _dao;

        public RefreshTokensRepository(RassvetDBContext dao)
        {
            _dao = dao;
        }

        public async Task Add(RefreshToken token)
        {
            _dao.RefreshTokens.Add(token);
            await _dao.SaveChangesAsync();
        }

        public async Task<RefreshToken> GetByID(int ID)
        {
            return await _dao.RefreshTokens.FindAsync(ID);
        }

        public async Task<RefreshToken> GetByToken(string token)
        {
            return await _dao.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
        }

        public async Task<RefreshToken> GetByUserID(int UserID)
        {
            return await _dao.RefreshTokens.FirstOrDefaultAsync(t => t.UserId == UserID);
        }

        public async Task Remove(RefreshToken token)
        {
            var curToken = await GetByID(token.Id);

            if (curToken is null) return;

            _dao.RefreshTokens.Remove(curToken);
            _dao.SaveChanges();
        }

        public async Task RemoveAll(int UserID)
        {
            var tokens = _dao.RefreshTokens.ToList().FindAll(t => t.UserId == UserID);

            if (tokens is null) return;

            _dao.RefreshTokens.RemoveRange(tokens);
            _dao.SaveChanges();
        }

        public async Task UpdateToken(RefreshToken token, string newToken)
        {
            var curToken = await GetByID(token.Id);
            curToken.Token = newToken;
            _dao.SaveChanges();
        }
    }
}
