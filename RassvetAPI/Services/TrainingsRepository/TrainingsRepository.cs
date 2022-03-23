using Microsoft.EntityFrameworkCore;
using RassvetAPI.Models.RassvetDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Services.TrainingsRepository
{
    public class TrainingsRepository : ITrainingsRepository
    {
        private readonly RassvetDBContext _dao;

        public TrainingsRepository(RassvetDBContext dao)
        {
            _dao = dao;
        }

        public Task AddTraining(Training training)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Training>> GetAllTrainings()
            => await _dao.Trainings.ToListAsync(); 

        public async Task<List<Training>> GetClientTrainings(int clientID)
            => await _dao.Trainings
                .Where(t => t.Group.ClientToGroups
                .Any(c => c.ClientId == clientID))
                .ToListAsync();

        public async Task<List<Training>> GetGroupTrainings(int groupID)
            => await _dao.Trainings
                  .Where(t => t.GroupId == groupID)
                  .ToListAsync();

        public async Task<List<Training>> GetSectionTrainings(int sectionID)
            => await _dao.Trainings
                  .Where(t => t.Group.SectionId == sectionID)
                  .ToListAsync();

        public async Task<Training> GetTraining(int trainingID)
            => await _dao.Trainings.FindAsync(trainingID); 

        public Task RemoveTraining(Training training)
        {
            throw new NotImplementedException();
        }
    }
}
