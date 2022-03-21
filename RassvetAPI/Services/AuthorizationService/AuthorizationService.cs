
using Microsoft.EntityFrameworkCore;
using RassvetAPI.Models;
using RassvetAPI.Models.RassvetDBModels;
using RassvetAPI.Models.ResponseModels;
using RassvetAPI.Services.JwtToken;
using RassvetAPI.Services.PasswordHasher;
using RassvetAPI.Services.RefreshTokensRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Services.AuthorizationService
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IPasswordHasher _hasher;
        private readonly JwtAccessTokenGenerator _accessTokenGenerator;
        private readonly JwtRefreshTokenGenerator _refreshTokenGenerator;
        private readonly IRefreshTokensRepository _refreshTokensRepository;
        private readonly JwtRefreshTokenValidator _refreshTokenValidator;

        public AuthorizationService(IPasswordHasher hasher, JwtAccessTokenGenerator accessTokenGenerator, JwtRefreshTokenGenerator refreshTokenGenerator, IRefreshTokensRepository refreshTokensRepository, JwtRefreshTokenValidator refreshTokenValidator)
        {
            _hasher = hasher;
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _refreshTokensRepository = refreshTokensRepository;
            _refreshTokenValidator = refreshTokenValidator;
        }

        public async Task<TokensResponse> LogIn( LogInModel logInModel)
        {
            using RassvetDBContext _dao = new RassvetDBContext();
            var users = await _dao.Users.ToListAsync();
            var user = users.Find(u => u.Email == logInModel.Email && _hasher.Verify(logInModel.Password, u.Password));
            if (user is null) return null;

            var loginResponse = await Authorize(user);

            return loginResponse;
        }

        async Task<TokensResponse> Authorize(User user)
        {
            using RassvetDBContext _dao = new RassvetDBContext();
            var authorizeResponse = new TokensResponse
            {
                AccessToken = _accessTokenGenerator.GenerateToken(user),
                RefreshToken = _refreshTokenGenerator.GenerateToken(user)
            };

            var refreshTokenObj = new RefreshToken
            {
                UserId = user.Id,
                Token = authorizeResponse.RefreshToken
            };

            if (user.RefreshTokens.Count() > 5)
            {
                var toDelete = user.RefreshTokens.SkipLast(5).ToList();

                _dao.RefreshTokens.RemoveRange(toDelete);
                await _dao.SaveChangesAsync();
            }

            await _refreshTokensRepository.Add(refreshTokenObj);

            return authorizeResponse;
        }
        
        public async Task<TokensResponse> RefreshTokens(string oldRefreshToken)
        {
            using RassvetDBContext _dao = new RassvetDBContext();
            var refreshToken = await _refreshTokensRepository.GetByToken(oldRefreshToken);
            if (refreshToken is null) return null;

            var user = await _dao.Users.FindAsync(refreshToken.UserId);
            if (user is null) return null;

            if (!_refreshTokenValidator.Validate(oldRefreshToken))
            {
                await _refreshTokensRepository.Remove(refreshToken);
                return null;
            }

            if (user.RefreshTokens.Count() > 5)
            {
                var toDelete = user.RefreshTokens.SkipLast(5).ToList();

                _dao.RefreshTokens.RemoveRange(toDelete);
                await _dao.SaveChangesAsync();
            }

            var newToken = _refreshTokenGenerator.GenerateToken(user);
            await _refreshTokensRepository.UpdateToken(refreshToken, newToken);

            var tokensResponse = new TokensResponse
            {
                AccessToken = _accessTokenGenerator.GenerateToken(user),
                RefreshToken = newToken
            };

            return tokensResponse;
        }

        public async Task LogoutSession(string accessToken)
        {
            using RassvetDBContext _dao = new RassvetDBContext();
            var token = await _refreshTokensRepository.GetByToken(accessToken);
            if (token is null) return;

            await _refreshTokensRepository.Remove(token);
        }

        public async Task LogoutUser(int UserID)
        {
            using RassvetDBContext _dao = new RassvetDBContext();
            var user = await _dao.Users.FindAsync(UserID);
            if (user is null) return;

            await _refreshTokensRepository.RemoveAll(UserID);
        }
    }
}