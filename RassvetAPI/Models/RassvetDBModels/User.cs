using System;
using System.Collections.Generic;

#nullable disable

namespace RassvetAPI.Models.RassvetDBModels
{
    public partial class User
    {
        public User()
        {
            RefreshTokens = new HashSet<RefreshToken>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ClientInfo ClientInfo { get; set; }
        public virtual TrenerInfo TrenerInfo { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
