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

namespace RassvetAPI.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthorizationService _authService;
        private readonly IRegistrationService _registrationService;
        private readonly IRefreshTokensRepository _refreshTokenRepository;

        public AuthenticationController(IAuthorizationService loginingService, IRegistrationService registrationService, IRefreshTokensRepository refreahTokenRepository)
        {
            _authService = loginingService;
            _registrationService = registrationService;
            _refreshTokenRepository = refreahTokenRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] LogInModel logInModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            var logInResponse = await _authService.LogIn(logInModel);

            if (logInResponse is null) BadRequest();

            return Ok(logInResponse);
        }

        [HttpPost("register/client")]
        public async Task<IActionResult> RegisterUser([FromBody] ClientRegisterModel clientRegModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                await _registrationService.RegisterUser(clientRegModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("register/admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminRegisterModel adminRegModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                await _registrationService.RegisterAdmin(adminRegModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(
                new
                {
                    Token = await _authService.LogIn(new LogInModel { Email = adminRegModel.Email, Password = adminRegModel.Password })
                });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequestModel refreshRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            var newTokens = await _authService.RefreshTokens(refreshRequest.RefreshToken);
            if (newTokens is null) return BadRequest();

            return Ok(newTokens);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutSession([FromBody] RefreshRequestModel refreshRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _authService.LogoutSession(refreshRequest.RefreshToken);

            return Ok();
        }

        [Authorize]
        [HttpDelete("logoutAll")]
        public async Task<IActionResult> LogoutUser()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);

            await _authService.LogoutUser(id);

            return Ok();
        }

        [Authorize]
        [HttpGet("checkAuth")]
        public IActionResult ChekAuth()
        {
            return Ok();
        }
    }
}
