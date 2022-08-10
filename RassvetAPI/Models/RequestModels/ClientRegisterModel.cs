using RassvetAPI.Util.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Models.RequestModels
{
    public class ClientRegisterModel : UserRegisterModel
    {
        [Required]
        public string Surname { get; set; }

        [Required]
        public string Name { get; set; }

        public string Patronymic { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}
