using RassvetAPI.Models;
using RassvetAPI.Models.RassvetDBModels;
using RassvetAPI.Services.PasswordHasher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Services.RegistrationService
{
    public class RegistrationService : IRegistrationService
    {
        private readonly RassvetDBContext _dao;
        private readonly IPasswordHasher _hasher;

        public RegistrationService(RassvetDBContext dao, IPasswordHasher hasher)
        {
            _dao = dao;
            _hasher = hasher;
        }

        public async Task RegisterUser(ClientRegisterModel clientRegisterModel)
        {
            var user = new User
            {
                Email = clientRegisterModel.Email,
                Password = _hasher.HashPassword(clientRegisterModel.Password),
                RoleId = 1
            };

            var client = new ClientInfo
            {
                User = user,
                Surname = clientRegisterModel.Surname,
                Name = clientRegisterModel.Name,
                Patronymic = clientRegisterModel.Patronymic,
                BirthDate = clientRegisterModel.BirthDate
            };

            await _dao.ClientInfos.AddAsync(client);
            await _dao.SaveChangesAsync();
        }

        public async Task RegisterAdmin(AdminRegisterModel clientRegisterModel)
        {
            var user = new User
            {
                Email = clientRegisterModel.Email,
                Password = _hasher.HashPassword(clientRegisterModel.Password),
                RoleId = 4
            };

            await _dao.Users.AddAsync(user);
            await _dao.SaveChangesAsync();
        }
    }
}
