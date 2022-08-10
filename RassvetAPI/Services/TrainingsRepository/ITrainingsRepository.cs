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

        Task<List<Training>> GetClientActiveTrainingsAsync(int clientID);
        Task<List<Training>> GetClientPastTrainingsAsync(int clientID, int pagesCount);

        Task<List<Training>> GetClientActiveTrainingsBySectionAsync(int clientId, int sectionId);
        Task<List<Training>> GetClientPastTrainingsBySectionAsync(int clientId, int sectionId, int pagesCount);

        Task AddTrainingAsync(Training training);

        Task RemoveTrainingAsync(Training training);
    }
}
