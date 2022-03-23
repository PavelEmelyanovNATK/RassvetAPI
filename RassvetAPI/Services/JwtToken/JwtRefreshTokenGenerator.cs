using Microsoft.IdentityModel.Tokens;
using RassvetAPI.Models;
using RassvetAPI.Models.RassvetDBModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RassvetAPI.Services.JwtToken
{
    public class JwtRefreshTokenGenerator : IJwtTokenGenerator
    {
        JwtConfigurationModel _jwtConfiguration;
        public JwtRefreshTokenGenerator(JwtConfigurationModel jwtConfiguration)
        {
            _jwtConfiguration = jwtConfiguration;
        }

        public string GenerateToken(User user)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfiguration.RefreshTokenSecret));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("ID", user.Id.ToString())

            };

            var token = new JwtSecurityToken(
                _jwtConfiguration.Issuer,
                _jwtConfiguration.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(_jwtConfiguration.RefreshTokenExpirationMinutes),
                signingCredentials);

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodedToken;
        }
    }
}
