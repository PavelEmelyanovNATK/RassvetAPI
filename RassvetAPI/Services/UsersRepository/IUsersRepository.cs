using RassvetAPI.Models.RassvetDBModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RassvetAPI.Services.UsersRepository
{
    public interface IUsersRepository
    {
        Task AddUserAsync(User client);
        Task<User> GetUserByIDAsync(int ID);
        Task<User> GetUserByEmailAsync(string email);
        Task RemoveUserAsync(int clientId);
        Task<List<User>> GetAllUsersAsync();
    }
}
