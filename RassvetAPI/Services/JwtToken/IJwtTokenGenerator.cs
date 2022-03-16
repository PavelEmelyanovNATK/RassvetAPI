using RassvetAPI.Models.RassvetDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Services.JwtToken
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
