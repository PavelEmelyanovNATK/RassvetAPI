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
        public async Task Add(ClientInfo client)
        {
            using RassvetDBContext _dao = new RassvetDBContext();
            await _dao.ClientInfos.AddAsync(client);
            await _dao.SaveChangesAsync();
        }

        public async Task Edit(ClientInfo client, EditClientModel newClientInfo)
        {
            using RassvetDBContext _dao = new RassvetDBContext();
            var curClient = await _dao.ClientInfos.FindAsync(client.UserId);
            if (curClient is null) return;

            curClient.Surname = newClientInfo.Surname;
            curClient.Name = newClientInfo.Name;
            curClient.Patronymic = newClientInfo.Patronymic;
            curClient.BirthDate = newClientInfo.BirthDate;

            await _dao.SaveChangesAsync();
        }

        public async Task<List<ClientInfo>> GetAllClients()
        {
            using RassvetDBContext _dao = new RassvetDBContext();
            return await _dao.ClientInfos.ToListAsync();
        }

        public async Task<ClientInfo> GetClientByEmail(string email)
        {
            using RassvetDBContext _dao = new RassvetDBContext();
            return await _dao.ClientInfos.FirstOrDefaultAsync(client => client.User.Email == email);
        }

        public async Task<ClientInfo> GetClientByID(int ID)
        {
            using RassvetDBContext _dao = new RassvetDBContext();
            return await _dao.ClientInfos.FirstOrDefaultAsync(client => client.UserId == ID); 
        }
        

        public async Task Remove(ClientInfo client)
        {
            using RassvetDBContext _dao = new RassvetDBContext();
            var curClient = await _dao.Users.FindAsync(client.UserId);
            if (curClient is null) return;

            _dao.Users.Remove(curClient);
        }
    }
}
