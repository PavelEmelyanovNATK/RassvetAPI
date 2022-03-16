using RassvetAPI.Models;
using RassvetAPI.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Services.AuthorizationService
{
    public interface IAuthorizationService
    {
        Task<TokensResponse> LogIn(LogInModel logInModel);
        Task<TokensResponse> RefreshTokens(string oldRefreshToken);
        Task LogoutSession(string accessToken);
        Task LogoutUser(int UserID);
    }
}
