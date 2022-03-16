using RassvetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Services.RegistrationService
{
    public interface IRegistrationService
    {
        Task RegisterUser(ClientRegisterModel userRegisterModel);
        Task RegisterAdmin(AdminRegisterModel clientRegisterModel);
    }
}
