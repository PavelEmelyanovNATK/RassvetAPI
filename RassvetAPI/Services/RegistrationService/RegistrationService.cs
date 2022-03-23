using Microsoft.EntityFrameworkCore;
using RassvetAPI.Models.RassvetDBModels;
using RassvetAPI.Models.RequestModels;
using RassvetAPI.Services.PasswordHasher;
using System;
using System.Threading.Tasks;

namespace RassvetAPI.Services.RegistrationService
{
    public class RegistrationService : IRegistrationService
    {
        private readonly RassvetDBContext _dao;
        private readonly IPasswordHasher _hasher;

        public RegistrationService(
            IPasswordHasher hasher, 
            RassvetDBContext dao
            )
        {
            _hasher = hasher;
            _dao = dao;
        }

        public async Task RegisterUser(ClientRegisterModel clientRegisterModel)
        {
            var userInfo = new User
            {
                Email = clientRegisterModel.Email,
                Password = _hasher.HashPassword(clientRegisterModel.Password),
                RoleId = 1
            };

            var client = new ClientInfo
            {
                User = userInfo,
                Surname = clientRegisterModel.Surname,
                Name = clientRegisterModel.Name,
                Patronymic = clientRegisterModel.Patronymic,
                BirthDate = clientRegisterModel.BirthDate
            };

            try
            {
                await _dao.ClientInfos.AddAsync(client);
                await _dao.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new EmailAlreadyTakenException("Пользователь с таким Email уже существует.");
            }
        }

        public async Task RegisterAdmin(AdminRegisterModel clientRegisterModel)
        {
            var user = new User
            {
                Email = clientRegisterModel.Email,
                Password = _hasher.HashPassword(clientRegisterModel.Password),
                RoleId = 4
            };

            try
            {
                await _dao.Users.AddAsync(user);
                await _dao.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new EmailAlreadyTakenException("Пользователь с таким Email уже существует.");
            }
        }
    }

    public abstract class RegistrationException : Exception
    {
        public RegistrationException() : base() { }
        public RegistrationException(string message) : base(message) { }
    }

    public class EmailAlreadyTakenException : RegistrationException
    {
        public EmailAlreadyTakenException() : base() { }
        public EmailAlreadyTakenException(string message) : base(message) { }
    }
}
