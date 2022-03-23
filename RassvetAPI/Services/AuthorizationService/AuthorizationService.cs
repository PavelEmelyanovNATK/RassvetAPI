using Microsoft.EntityFrameworkCore;
using RassvetAPI.Models.RassvetDBModels;
using RassvetAPI.Models.RequestModels;
using RassvetAPI.Models.ResponseModels;
using RassvetAPI.Services.JwtToken;
using RassvetAPI.Services.PasswordHasher;
using RassvetAPI.Services.RefreshTokensRepository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Services.AuthorizationService
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly RassvetDBContext _dao;
        private readonly IPasswordHasher _hasher;
        private readonly JwtAccessTokenGenerator _accessTokenGenerator;
        private readonly JwtRefreshTokenGenerator _refreshTokenGenerator;
        private readonly IRefreshTokensRepository _refreshTokensRepository;
        private readonly JwtRefreshTokenValidator _refreshTokenValidator;

        public AuthorizationService(
            IPasswordHasher hasher,
            JwtAccessTokenGenerator accessTokenGenerator,
            JwtRefreshTokenGenerator refreshTokenGenerator,
            IRefreshTokensRepository refreshTokensRepository,
            JwtRefreshTokenValidator refreshTokenValidator,
            RassvetDBContext dao
            )
        {
            _hasher = hasher;
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _refreshTokensRepository = refreshTokensRepository;
            _refreshTokenValidator = refreshTokenValidator;
            _dao = dao;
        }

        public async Task<TokensResponse> LogInAsync(LogInModel logInModel)
        {
            var users = await _dao.Users.ToListAsync();
            var user = users.Find(u => u.Email == logInModel.Email && _hasher.Verify(logInModel.Password, u.Password));
            if (user is null) 
                throw new UserNotFoundException("Неверный логин или пароль.");

            var loginResponse = await Authorize(user);

            return loginResponse;
        }

        /// <summary>
        /// Генерирует токен доступа, содержащий роль и id пользователя, и токен обновления.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        async Task<TokensResponse> Authorize(User user)
        {
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

            //Ограничение на кол-во токенов обновления в базе данных.
            //Если токенов больше 5, удаляется самый ранний из существующих.
            if (user.RefreshTokens.Count() > 5)
            {
                var toDelete = user.RefreshTokens.SkipLast(5).ToList();

                _dao.RefreshTokens.RemoveRange(toDelete);
                await _dao.SaveChangesAsync();
            }

            await _refreshTokensRepository.Add(refreshTokenObj);

            return authorizeResponse;
        }

        public async Task<TokensResponse> RefreshTokensAsync(string oldRefreshToken)
        {
            var refreshToken = await _refreshTokensRepository.GetByToken(oldRefreshToken);
            if (refreshToken is null) 
                throw new UserAlreadyLogOutedException("Токен обновления не найден.");

            var user = await _dao.Users.FindAsync(refreshToken.UserId);
            if (user is null)
                throw new UserNotFoundException("Пользователь не найден.");

            if (!_refreshTokenValidator.Validate(oldRefreshToken))
            {
                await _refreshTokensRepository.Remove(refreshToken);
                throw new InvalidRefreshTokenException("Неверный токен обновления.");
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

        public async Task LogoutSessionAsync(string accessToken)
        {
            var token = await _refreshTokensRepository.GetByToken(accessToken);
            if (token is null) return;

            await _refreshTokensRepository.Remove(token);
        }

        public async Task LogoutUserAsync(int UserID)
        {
            var user = await _dao.Users.FindAsync(UserID);
            if (user is null) return;

            await _refreshTokensRepository.RemoveAll(UserID);
        }
    }

    public abstract class AuthException : Exception {
        public AuthException() : base() { }
        public AuthException(string message) : base(message) { }
    }

    public class UserNotFoundException : AuthException
    {
        public UserNotFoundException() : base() { }
        public UserNotFoundException(string message) : base(message) { }
    }

    public class UserAlreadyLogOutedException : AuthException
    {
        public UserAlreadyLogOutedException() : base(){ }
        public UserAlreadyLogOutedException(string message) : base(message) { }
    }

    public class InvalidRefreshTokenException : AuthException
    {
        public InvalidRefreshTokenException() : base() { }
        public InvalidRefreshTokenException(string message) : base(message) { }
    }
}