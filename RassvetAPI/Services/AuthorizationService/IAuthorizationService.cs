using RassvetAPI.Models.RequestModels;
using RassvetAPI.Models.ResponseModels;
using System.Threading.Tasks;

namespace RassvetAPI.Services.AuthorizationService
{
    public interface IAuthorizationService
    {
        /// <summary>
        /// Производит авторизацию и аутентификацию пользователя в системе.
        /// При успешной авторизации возвращает токен доступа и токен обновления.
        /// </summary>
        /// <param name="logInModel"></param>
        /// <returns>Токены доступа и обновления.</returns>
        /// <exception cref="UserNotFoundException"></exception>
        Task<TokensResponse> LogInAsync(LogInModel logInModel);

        /// <summary>
        /// Генерирует новые токены доступа и обновления.
        /// </summary>
        /// <param name="oldRefreshToken"></param>
        /// <returns></returns>
        /// <exception cref="UserAlreadyLogOutedException">Возникает если передаваемый токен обновления не был найден в базе данных.</exception>
        /// <exception cref="UserNotFoundException"></exception>
        /// <exception cref="InvalidRefreshTokenException"></exception>
        Task<TokensResponse> RefreshTokensAsync(string oldRefreshToken);

        /// <summary>
        /// Удаляет передаваемый токен обновления из базы данных.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        Task LogoutSessionAsync(string accessToken);

        /// <summary>
        /// Удаляет все токены обновления пользователя.
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        Task LogoutUserAsync(int UserID);
    }
}
