using Microsoft.EntityFrameworkCore;
using RassvetAPI.Models.RassvetDBModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RassvetAPI.Services.ClientsRepository
{
    public class ClientsRepository : IClientsRepository
    {
        private readonly RassvetDBContext _dao;

        public ClientsRepository(RassvetDBContext dao)
        {
            _dao = dao;
        }

        public async Task AddClientAsync(ClientInfo client)
        {
            await _dao.ClientInfos.AddAsync(client);
            await _dao.SaveChangesAsync();
        }

        public async Task ChangeBirthDateAsync(int clientId, DateTime newBirthDate)
        {
            (await GetClientByIDAsync(clientId)).BirthDate = newBirthDate;
            await _dao.SaveChangesAsync();
        }

        public async Task ChangeNameAsync(int clientId, string newName)
        {
            (await GetClientByIDAsync(clientId)).Name = newName;
            await _dao.SaveChangesAsync();
        }

        public async Task ChangePatronymicAsync(int clientId, string newPatronymic)
        {
            (await GetClientByIDAsync(clientId)).Patronymic = newPatronymic;
            await _dao.SaveChangesAsync();
        }

        public async Task ChangeSurnameAsync(int clientId, string newSurname)
        {
            (await GetClientByIDAsync(clientId)).Surname = newSurname;
            await _dao.SaveChangesAsync();
        }

        public async Task<List<ClientInfo>> GetAllClientsAsync()
            => await _dao.ClientInfos.ToListAsync();

        public async Task<ClientInfo> GetClientByEmailAsync(string email)
            => await _dao.ClientInfos.FirstOrDefaultAsync(client => client.User.Email == email);

        public async Task<ClientInfo> GetClientByIDAsync(int ID)
            => await _dao.ClientInfos.FirstOrDefaultAsync(client => client.UserId == ID); 

        public async Task RemoveAsync(int clientId)
        {
            var curClient = await GetClientByIDAsync(clientId);
            if (curClient is null) return;

            _dao.ClientInfos.Remove(curClient);
            _dao.Users.Remove(curClient.User);
            await _dao.SaveChangesAsync();
        }
    }
}
