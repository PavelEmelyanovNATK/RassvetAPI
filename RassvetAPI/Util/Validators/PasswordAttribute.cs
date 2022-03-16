using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Util.Validators
{
    public class PasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var password = value?.ToString();

            if (password is null) return false;

            return
                password.Length >= 6
                &&
                password.Any(c => char.IsDigit(c))
                &&
                password.Any(c => char.IsLetter(c))
                &&
                password.Any(c => char.IsUpper(c));
        }
    }
}
