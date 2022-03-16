using RassvetAPI.Models.RassvetDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Services.RefreshTokensRepository
{
    public interface IRefreshTokensRepository
    {
        Task Add(RefreshToken token);
        Task<RefreshToken> GetByID(int ID);
        Task<RefreshToken> GetByUserID(int UserID);
        Task<RefreshToken> GetByToken(string token);
        Task Remove(RefreshToken token);
        Task RemoveAll(int UserID);
        Task UpdateToken(RefreshToken token, string newToken);
    }
}
