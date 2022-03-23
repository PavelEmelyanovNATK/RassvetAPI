using RassvetAPI.Models.RassvetDBModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RassvetAPI.Services.ClientsRepository
{
    public interface IClientsRepository
    {
        Task AddClient(ClientInfo client);
        Task<ClientInfo> GetClientByID(int ID);
        Task<ClientInfo> GetClientByEmail(string email);
        Task Remove(int clientId);
        Task<List<ClientInfo>> GetAllClients();

        Task ChangeName(int clientId, string newName);
        Task ChangeSurname(int clientId, string newSurname);
        Task ChangePatronymic(int clientId, string newPatronymic);
        Task ChangeBirthDate(int clientId, DateTime newBirthDate);
    }
}
