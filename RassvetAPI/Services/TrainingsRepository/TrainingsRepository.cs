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
        private const int TRAININGS_PER_PAGE = 30;
        private readonly RassvetDBContext _dao;

        public TrainingsRepository(RassvetDBContext dao)
        {
            _dao = dao;
        }

        public Task AddTrainingAsync(Training training)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Training>> GetAllTrainingsAsync()
            => await _dao.Trainings.ToListAsync();

        public async Task<List<Training>> GetClientActiveTrainingsAsync(int clientID)
            => await _dao.Trainings
            .Where(t => t.Group.ClientToGroups.Any(ct => ct.ClientId == clientID))
            .Where(t => t.StartDate.AddMinutes(t.DurationInMinutes) >= DateTime.Now)
            .ToListAsync();

        public async Task<List<Training>> GetClientActiveTrainingsBySectionAsync(int clientId, int sectionId)
             => await _dao.Trainings
            .Where(t => t.Group.ClientToGroups.Any(ct => ct.ClientId == clientId))
            .Where(t => t.Group.SectionId == sectionId)
            .Where(t => t.StartDate.AddMinutes(t.DurationInMinutes) >= DateTime.Now)
            .ToListAsync();


        public async Task<List<Training>> GetClientPastTrainingsAsync(int clientID, int pagesCount)
            => await _dao.Trainings
            .Where(t => t.Group.ClientToGroups.Any(ct => ct.ClientId == clientID))
            .Where(t => t.StartDate.AddMinutes(t.DurationInMinutes) < DateTime.Now)
            .Take(pagesCount * TRAININGS_PER_PAGE)
            .ToListAsync();

        public async Task<List<Training>> GetClientPastTrainingsBySectionAsync(int clientId, int sectionId, int pagesCount)
            => await _dao.Trainings
            .Where(t => t.Group.ClientToGroups.Any(ct => ct.ClientId == clientId))
            .Where(t => t.Group.SectionId == sectionId)
            .Where(t => t.StartDate.AddMinutes(t.DurationInMinutes) < DateTime.Now)
            .Take(pagesCount * TRAININGS_PER_PAGE)
            .ToListAsync();

        public async Task<List<Training>> GetClientTrainingsAsync(int clientID)
            => await _dao.Trainings
            .Where(t => t.Group.ClientToGroups.Any(c => c.ClientId == clientID))
            .ToListAsync();

        public async Task<List<Training>> GetGroupTrainingsAsync(int groupID)
            => await _dao.Trainings
                  .Where(t => t.GroupId == groupID)
                  .ToListAsync();

        public async Task<List<Training>> GetSectionTrainingsAsync(int sectionID)
            => await _dao.Trainings
                  .Where(t => t.Group.SectionId == sectionID)
                  .ToListAsync();

        public async Task<Training> GetTrainingAsync(int trainingID)
            => await _dao.Trainings.FindAsync(trainingID);

        public Task RemoveTrainingAsync(Training training)
        {
            throw new NotImplementedException();
        }
    }
}
