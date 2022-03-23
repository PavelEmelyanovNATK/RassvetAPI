using RassvetAPI.Models.RequestModels;
using System.Threading.Tasks;

namespace RassvetAPI.Services.RegistrationService
{
    public interface IRegistrationService
    {
        /// <summary>
        /// Регистрирует клиента в системе.
        /// </summary>
        /// <param name="userRegisterModel"></param>
        /// <returns></returns>
        /// <exception cref="EmailAlreadyTakenException"></exception>
        Task RegisterUser(ClientRegisterModel userRegisterModel);

        /// <summary>
        /// Регистрирует администратора в системе.
        /// </summary>
        /// <param name="clientRegisterModel"></param>
        /// <returns></returns>
        /// <exception cref="EmailAlreadyTakenException"></exception>
        Task RegisterAdmin(AdminRegisterModel clientRegisterModel);
    }
}
