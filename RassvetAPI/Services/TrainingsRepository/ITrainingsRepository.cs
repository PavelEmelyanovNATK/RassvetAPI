using RassvetAPI.Models;
using RassvetAPI.Models.RassvetDBModels;
using RassvetAPI.Models.RassvetDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Services.TrainingsRepository
{
    public interface ITrainingsRepository
    {
        Task<Training> GetTraining(int trainingID);

        Task<List<Training>> GetAllTrainings();

        Task<List<Training>> GetSectionTrainings(int sectionID);

        Task<List<Training>> GetGroupTrainings(int groupID);

        Task<List<Training>> GetClientTrainings(int clientID);

        Task AddTraining(Training training);

        Task RemoveTraining(Training training);
    }
}
