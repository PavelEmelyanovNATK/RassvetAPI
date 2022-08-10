using Microsoft.EntityFrameworkCore;
using RassvetAPI.Models.RassvetDBModels;
using RassvetAPI.Models.RequestModels;
using RassvetAPI.Services.ClientsRepository;
using RassvetAPI.Services.PasswordHasher;
using RassvetAPI.Services.UsersRepository;
using System;
using System.Threading.Tasks;

namespace RassvetAPI.Services.RegistrationService
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IPasswordHasher _hasher;
        private readonly IUsersRepository _usersRepository;
        private readonly IClientsRepository _clientsRepository;

        public RegistrationService(
            IPasswordHasher hasher,
            IUsersRepository usersRepository,
            IClientsRepository clientsRepository)
        {
            _hasher = hasher;
            _usersRepository = usersRepository;
            _clientsRepository = clientsRepository;
        }

        public async Task RegisterClientAsync(ClientRegisterModel clientRegisterModel)
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
                await _clientsRepository.AddClientAsync(client);
            }
            catch (DbUpdateException)
            {
                throw new EmailAlreadyTakenException("Пользователь с таким Email уже существует.");
            }
        }

        public async Task RegisterAdminAsync(AdminRegisterModel clientRegisterModel)
        {
            var user = new User
            {
                Email = clientRegisterModel.Email,
                Password = _hasher.HashPassword(clientRegisterModel.Password),
                RoleId = 4
            };

            try
            {
                await _usersRepository.AddUserAsync(user);
            }
            catch (DbUpdateException)
            {
                throw new EmailAlreadyTakenException("Пользователь с таким Email уже существует.");
            }
        }

        public async Task RegisterManagerAsync(AdminRegisterModel managerRegisterModel)
        {
            var user = new User
            {
                Email = managerRegisterModel.Email,
                Password = _hasher.HashPassword(managerRegisterModel.Password),
                RoleId = 3
            };

            try
            {
                await _usersRepository.AddUserAsync(user);
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
