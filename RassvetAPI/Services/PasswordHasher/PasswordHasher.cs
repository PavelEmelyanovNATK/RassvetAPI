using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Services.PasswordHasher
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password) => 
            BCrypt.Net.BCrypt.HashPassword(password);

        public async Task<string> HashPasswordAsync(string password) => 
            await Task.Run(() => BCrypt.Net.BCrypt.HashPassword(password));

        public bool Verify(string password, string hash) => 
            BCrypt.Net.BCrypt.Verify(password, hash);

        public async Task<bool> VerifyAsync(string password, string hash) => 
            await Task.Run(() => BCrypt.Net.BCrypt.Verify(password, hash));
    }
}
