using RassvetAPI.Models;
using RassvetAPI.Models.RassvetDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Services.ClientTrainingsRepository
{
    public interface ITrainingsRepository
    {
        Task<Training> GetTraining(int ID);

        Task<ClientTrainingShortResonse> GetTrainingInfoForClient(Training training);

        //Task<List<ClientTrainingInfo>> GetAllClientTrainings(int clientID);

        Task<List<ClientTrainingShortResonse>> GetAllClientTrainings(ClientInfo client);

        //Task<List<ClientTrainingInfo>> GetSectionClientTrainings(int clientID, int sectionID);

        Task<List<ClientTrainingShortResonse>> GetSectionClientTrainings(ClientInfo client, Section section);

        Task AddTraining(Training training);

        Task RemoveTraining(Training training);

        //Task AddTrainingToClient(int clientID, int trainigID);

        Task AddTrainingToClient(ClientInfo client, Training training);

        Task RemoveTrainigFromClient(ClientInfo client, Training training);


    }
}
