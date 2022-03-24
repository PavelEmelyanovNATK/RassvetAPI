using RassvetAPI.Models.RassvetDBModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RassvetAPI.Services.TrainingsRepository
{
    public interface ITrainingsRepository
    {
        Task<Training> GetTrainingAsync(int trainingID);

        Task<List<Training>> GetAllTrainingsAsync();

        Task<List<Training>> GetSectionTrainingsAsync(int sectionID);

        Task<List<Training>> GetGroupTrainingsAsync(int groupID);

        Task<List<Training>> GetClientTrainingsAsync(int clientID);

        Task AddTrainingAsync(Training training);

        Task RemoveTrainingAsync(Training training);
    }
}
