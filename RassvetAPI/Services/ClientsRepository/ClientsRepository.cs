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

        public async Task AddClient(ClientInfo client)
        {
            await _dao.ClientInfos.AddAsync(client);
            await _dao.SaveChangesAsync();
        }

        public async Task ChangeBirthDate(int clientId, DateTime newBirthDate)
        {
            (await GetClientByID(clientId)).BirthDate = newBirthDate;
            await _dao.SaveChangesAsync();
        }

        public async Task ChangeName(int clientId, string newName)
        {
            (await GetClientByID(clientId)).Name = newName;
            await _dao.SaveChangesAsync();
        }

        public async Task ChangePatronymic(int clientId, string newPatronymic)
        {
            (await GetClientByID(clientId)).Patronymic = newPatronymic;
            await _dao.SaveChangesAsync();
        }

        public async Task ChangeSurname(int clientId, string newSurname)
        {
            (await GetClientByID(clientId)).Surname = newSurname;
            await _dao.SaveChangesAsync();
        }

        public async Task<List<ClientInfo>> GetAllClients()
            => await _dao.ClientInfos.ToListAsync();

        public async Task<ClientInfo> GetClientByEmail(string email)
            => await _dao.ClientInfos.FirstOrDefaultAsync(client => client.User.Email == email);

        public async Task<ClientInfo> GetClientByID(int ID)
            => await _dao.ClientInfos.FirstOrDefaultAsync(client => client.UserId == ID); 

        public async Task Remove(int clientId)
        {
            var curClient = await GetClientByID(clientId);
            if (curClient is null) return;

            _dao.ClientInfos.Remove(curClient);
            _dao.Users.Remove(curClient.User);
            await _dao.SaveChangesAsync();
        }
    }
}
