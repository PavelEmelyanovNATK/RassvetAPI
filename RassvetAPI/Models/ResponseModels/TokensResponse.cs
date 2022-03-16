using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Models.ResponseModels
{
    public class TokensResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
