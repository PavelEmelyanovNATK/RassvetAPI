using RassvetAPI.Models.RassvetDBModels;

namespace RassvetAPI.Services.JwtToken
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
