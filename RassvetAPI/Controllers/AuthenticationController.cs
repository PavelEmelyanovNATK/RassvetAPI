using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RassvetAPI.Models;
using RassvetAPI.Services.JwtToken;
using RassvetAPI.Services.AuthorizationService;
using RassvetAPI.Services.PasswordHasher;
using RassvetAPI.Services.RegistrationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IAuthorizationService = RassvetAPI.Services.AuthorizationService.IAuthorizationService;
using RassvetAPI.Services.RefreshTokensRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RassvetAPI.Models.RequestModels;

namespace RassvetAPI.Controllers
{
    /// <summary>
    /// Контроллер для авторизации и регистрации пользователей.
    /// </summary>
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthorizationService _authService;
        private readonly IRegistrationService _registrationService;
        private readonly IRefreshTokensRepository _refreshTokenRepository;

        public AuthenticationController(
            IAuthorizationService loginingService, 
            IRegistrationService registrationService, 
            IRefreshTokensRepository refreahTokenRepository
            )
        {
            _authService = loginingService;
            _registrationService = registrationService;
            _refreshTokenRepository = refreahTokenRepository;
        }

        /// <summary>
        /// Выполняет вход с сисему. В случае успешной авторизации вернёт
        /// токен доступа и токен обновления.
        /// </summary>
        /// <param name="logInModel"></param>
        /// <returns>Токен доступа и токен обновления.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> LogInAsync([FromBody] LogInModel logInModel)
        {
            try
            {
                return Ok(await _authService.LogInAsync(logInModel));
            }
            catch (AuthException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Метод для регистрации клиента в системе.
        /// </summary>
        /// <param name="clientRegModel"></param>
        /// <returns></returns>
        [HttpPost("register/client")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] ClientRegisterModel clientRegModel)
        {
            try
            {
                await _registrationService.RegisterUserAsync(clientRegModel);
            }
            catch (RegistrationException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        /// <summary>
        /// Метод для регистрации администратора в системе.
        /// Доступен только для авторизированных пользователей с ролью "Admin".
        /// </summary>
        /// <param name="adminRegModel"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("register/admin")]
        public async Task<IActionResult> RegisterAdminAsync([FromBody] AdminRegisterModel adminRegModel)
        {
            try
            {
                await _registrationService.RegisterAdminAsync(adminRegModel);
            }
            catch (RegistrationException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        /// <summary>
        /// Возвращает новые токен доступа и токен обновления.
        /// </summary>
        /// <param name="refreshRequest">Токен обновления.</param>
        /// <returns>Новые токен доступа и токен обновления.</returns>
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokensAsync([FromBody] RefreshRequestModel refreshRequest)
        {
            try
            {
                return Ok(await _authService.RefreshTokensAsync(refreshRequest.RefreshToken));
            }
            catch (AuthException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Выполняет выход пользователя из системы. Токен обновления теряет свою силу.
        /// </summary>
        /// <param name="refreshRequest">Токен обновления.</param>
        /// <returns></returns>
        [HttpPost("logout")]
        public async Task<IActionResult> LogoutSession([FromBody] RefreshRequestModel refreshRequest)
        {
            await _authService.LogoutSessionAsync(refreshRequest.RefreshToken);

            return Ok();
        }

        /// <summary>
        /// Выполняет выход пользователя из системы для всех устройств.
        /// Все токены обновления теряют свою силу.
        /// Доступен только для авторизированных пользователей.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("logoutAll")]
        public async Task<IActionResult> LogoutUser()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);

            await _authService.LogoutUserAsync(id);

            return Ok();
        }

        /// <summary>
        /// Метод для проверки действительности авторизации.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("checkAuth")]
        public IActionResult ChekAuth()
        {
            return Ok();
        }
    }
}
