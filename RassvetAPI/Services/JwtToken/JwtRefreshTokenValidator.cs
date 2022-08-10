using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RassvetAPI.Models;
using System.IdentityModel.Tokens.Jwt;

namespace RassvetAPI.Services.JwtToken
{
    public class JwtRefreshTokenValidator
    {
        private readonly JwtConfigurationModel _jwtConfigurationModel;

        public JwtRefreshTokenValidator(JwtConfigurationModel configuration)
        {
            _jwtConfigurationModel = configuration;
        }

        public bool Validate(string refreshToken)
        {
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                // укзывает, будет ли валидироваться издатель при валидации токена
                ValidateIssuer = true,
                // строка, представляющая издателя
                ValidIssuer = _jwtConfigurationModel.Issuer,

                // будет ли валидироваться потребитель токена
                ValidateAudience = true,
                // установка потребителя токена
                ValidAudience = _jwtConfigurationModel.Audience,
                // будет ли валидироваться время существования
                ValidateLifetime = true,

                // установка ключа безопасности
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfigurationModel.RefreshTokenSecret)),
                // валидация ключа безопасности
                ValidateIssuerSigningKey = true,

                ClockSkew = TimeSpan.Zero
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                jwtSecurityTokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
