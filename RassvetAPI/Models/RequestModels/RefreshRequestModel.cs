using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Models
{
    public class RefreshRequestModel
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
