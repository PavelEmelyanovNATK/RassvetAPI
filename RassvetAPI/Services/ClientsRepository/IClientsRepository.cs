using RassvetAPI.Models.RassvetDBModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RassvetAPI.Services.ClientsRepository
{
    public interface IClientsRepository
    {
        Task AddClientAsync(ClientInfo client);
        Task<ClientInfo> GetClientByIDAsync(int ID);
        Task<ClientInfo> GetClientByEmailAsync(string email);
        Task RemoveAsync(int clientId);
        Task<List<ClientInfo>> GetAllClientsAsync();

        Task ChangeNameAsync(int clientId, string newName);
        Task ChangeSurnameAsync(int clientId, string newSurname);
        Task ChangePatronymicAsync(int clientId, string newPatronymic);
        Task ChangeBirthDateAsync(int clientId, DateTime newBirthDate);
    }
}
