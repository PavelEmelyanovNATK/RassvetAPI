using RassvetAPI.Models.RassvetDBModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RassvetAPI.Services.UsersRepository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly RassvetDBContext _dao;

        public UsersRepository(RassvetDBContext dao)
        {
            _dao = dao;
        }

        public async Task AddUserAsync(User user)
        {
            await _dao.Users.AddAsync(user);
            await _dao.SaveChangesAsync();
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetUserByIDAsync(int ID)
            => await _dao.Users.FindAsync(ID);

        public Task RemoveUserAsync(int clientId)
        {
            throw new System.NotImplementedException();
        }
    }
}
