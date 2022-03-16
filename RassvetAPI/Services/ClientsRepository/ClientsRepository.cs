using Microsoft.EntityFrameworkCore;
using RassvetAPI.Models;
using RassvetAPI.Models.RassvetDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task Add(ClientInfo client)
        {
            await _dao.ClientInfos.AddAsync(client);
            await _dao.SaveChangesAsync();
        }

        public async Task Edit(ClientInfo client, EditClientModel newClientInfo)
        {
            var curClient = await _dao.ClientInfos.FindAsync(client.UserId);
            if (curClient is null) return;

            curClient.Surname = newClientInfo.Surname;
            curClient.Name = newClientInfo.Name;
            curClient.Patronymic = newClientInfo.Patronymic;
            curClient.BirthDate = newClientInfo.BirthDate;

            await _dao.SaveChangesAsync();
        }

        public async Task<ICollection<ClientInfo>> GetAllClients()
        {
            return await _dao.ClientInfos.ToListAsync();
        }

        public async Task<ClientInfo> GetByEmail(string email)
        {
            return await _dao.ClientInfos.FirstOrDefaultAsync(client => client.User.Email == email);
        }

        public async Task<ClientInfo> GetByID(int ID)
        {
            return await _dao.ClientInfos.FirstOrDefaultAsync(client => client.UserId == ID);
        }

        public async Task Remove(ClientInfo client)
        {
            var curClient = await _dao.Users.FindAsync(client.UserId);
            if (curClient is null) return;

            _dao.Users.Remove(curClient);
        }
    }
}
