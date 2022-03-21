using RassvetAPI.Models;
using RassvetAPI.Models.RassvetDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Services.ClientsRepository
{
    public interface IClientsRepository
    {
        Task Add(ClientInfo client);
        Task<ClientInfo> GetClientByID(int ID);
        Task<ClientInfo> GetClientByEmail(string email);
        Task Edit(ClientInfo client, EditClientModel newClientInfo);
        Task Remove(ClientInfo client);
        Task<List<ClientInfo>> GetAllClients();
    }
}
