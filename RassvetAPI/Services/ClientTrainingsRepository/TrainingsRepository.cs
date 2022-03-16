using Microsoft.EntityFrameworkCore;
using RassvetAPI.Models;
using RassvetAPI.Models.RassvetDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Services.ClientTrainingsRepository
{
    public class TrainingsRepository : ITrainingsRepository
    {
        private readonly RassvetDBContext _dao;

        public TrainingsRepository(RassvetDBContext dao)
        {
            _dao = dao;
        }

        public async Task AddTraining(Training training)
        {
            await _dao.Trainings.AddAsync(training);
            await _dao.SaveChangesAsync();
        }

        public async Task AddTrainingToClient(ClientInfo client, Training training)
        {
            client.ClientToTrainings.Add(new ClientToTraining { ClientId = client.UserId, TrainingId = training.Id });
            await _dao.SaveChangesAsync();
        }

        public Task<List<ClientTrainingShortResonse>> GetAllClientTrainings(ClientInfo client)
        {
            throw new NotImplementedException();
        }

        public Task<List<ClientTrainingShortResonse>> GetSectionClientTrainings(ClientInfo client, Section section)
        {
            throw new NotImplementedException();
        }

        public async Task<Training> GetTraining(int ID)
        {
            var training = await _dao.Trainings.FindAsync(ID);
            return training;
        }

        public Task<ClientTrainingShortResonse> GetTrainingInfoForClient(Training training)
        {
            throw new NotImplementedException();
        }

        public Task RemoveTrainigFromClient(ClientInfo client, Training training)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveTraining(Training training)
        {
            _dao.Trainings.Remove(training);
            await _dao.SaveChangesAsync();
        }
    }
}
