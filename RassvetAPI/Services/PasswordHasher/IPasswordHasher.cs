using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Services.PasswordHasher
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);

        Task<string> HashPasswordAsync(string password);

        bool Verify(string password, string hash);

        Task<bool> VerifyAsync(string password, string hash);
    }
}
