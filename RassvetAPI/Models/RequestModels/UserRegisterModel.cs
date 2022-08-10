using RassvetAPI.Util.Validators;
using System.ComponentModel.DataAnnotations;

namespace RassvetAPI.Models.RequestModels
{
    public class UserRegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Password]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
