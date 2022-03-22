using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RassvetAPI.Models;
using RassvetAPI.Models.RassvetDBModels;
using RassvetAPI.Models.ResponseModels;
using RassvetAPI.Services.ClientsRepository;
using RassvetAPI.Services.GroupsRepository;
using RassvetAPI.Services.SectionsRepository;
using RassvetAPI.Services.TrainingsRepository;
using RassvetAPI.Services.TrenersRepository;
using RassvetAPI.Util.UsefulExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Controllers
{
    /// <summary>
    /// Контроллер для авторизованного клиента. Предоставлят методы
    /// для получения информации, необходимой клиенту. Идентификатор
    /// клиента хранится в токене авторизации.
    /// </summary>
    [ApiController]
    [Authorize(Roles = "Client")]
    [Route("me")]
    public class ClientController : ControllerBase
    {
        private readonly IClientsRepository _clientsRepo;
        private readonly ISectionsRepository _sectionsRepository;
        private readonly ITrainingsRepository _trainingsRepository;
        private readonly ITrenersRepository _trenersRepository;
        private readonly IGroupsRepository _groupsRepository;

        public ClientController(
            IClientsRepository clientsRepo,
            ISectionsRepository sectionsRepository,
            ITrainingsRepository trainingsRepository,
            ITrenersRepository trenersRepository, 
            IGroupsRepository groupsRepository)
        {
            _clientsRepo = clientsRepo;
            _sectionsRepository = sectionsRepository;
            _trainingsRepository = trainingsRepository;
            _trenersRepository = trenersRepository;
            _groupsRepository = groupsRepository;
        }

        /// <summary>
        /// Возвращает всю информацию о клинете.
        /// </summary>
        /// <returns></returns>
        [HttpGet("info")]
        public async Task<IActionResult> GetClientInfo()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);
            var client = await _clientsRepo.GetClientByID(id);

            if (client is null) return Unauthorized();

            return Ok(new ClientInfoResponse
            {
                ID = client.UserId,
                Surname = client.Surname,
                Name = client.Name,
                Patronymic = client.Patronymic,
                BirthDate = client.BirthDate,
                RegistrationDate = client.RegistrationDate
            });
        }

        /*Пересмотреть решение о необходимости этого метода*/
        /// <summary>
        /// Возвращает список секций, в которых состоит клиент.
        /// </summary>
        /// <returns></returns>
        [HttpGet("sections")]
        public async Task<IActionResult> GetClientSections()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);
            var client = await _clientsRepo.GetClientByID(id);

            if (client is null) return Unauthorized();

            var sections = await _sectionsRepository.GetClientSections(id);

            return Ok(sections);
        }

        /// <summary>
        /// Возвращает список предстоящих тренрировок клинета.
        /// </summary>
        /// <returns>Список элементов с короткой информацией о тренировке.</returns>
        [HttpGet("active-trainings")]
        public async Task<IActionResult> GetClientActiveTrainings()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);
            var client = await _clientsRepo.GetClientByID(id);

            if (client is null) return Unauthorized();

            var trainings = (await _trainingsRepository.GetClientTrainings(id))
                .Where(t => t.StartDate.AddMinutes(t.DurationInMinutes) >= DateTime.Now);

            if (trainings is null) return Ok();

            SectionGroup group;
            TrenerInfo trener;
            Section section;

            return Ok(await Task.WhenAll(trainings
                .Select(async t =>
                {
                    group = await _groupsRepository.GetGroup(t.GroupId);
                    trener = await _trenersRepository.GetTrener(group.TrenerId);
                    section = await _sectionsRepository.GetSection(group.SectionId);

                    return new ClientTrainingShortResonse
                    {
                        ID = t.Id,
                        Title = t.Title,
                        DurationInMinutes = t.DurationInMinutes,
                        TrenerFullName = trener
                            .Let(tr => tr.Surname + " " + tr.Name + (tr.Patronymic is null ? "" : " " + tr.Patronymic)),
                        StartDate = t.StartDate,
                        SectionName = section.Name,
                        GroupName = group.Name
                    };
                }
                )));
        }

        /// <summary>
        /// Возвращает список прошедших тренировок клинета.
        /// </summary>
        /// <returns>Список элементов с короткой информацией о тренировке.</returns>
        [HttpGet("past-trainings")]
        public async Task<IActionResult> GetClientPastTrainings()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);
            var client = await _clientsRepo.GetClientByID(id);

            if (client is null) return Unauthorized();

            var trainings = (await _trainingsRepository.GetClientTrainings(id))
                .Where(t => t.StartDate.AddMinutes(t.DurationInMinutes) < DateTime.Now);

            if (trainings is null) return Ok();

            SectionGroup group;
            TrenerInfo trener;
            Section section;

            return Ok(await Task.WhenAll(trainings
                .Select(async t =>
                {
                    group = await _groupsRepository.GetGroup(t.GroupId);
                    trener = await _trenersRepository.GetTrener(group.TrenerId);
                    section = await _sectionsRepository.GetSection(group.SectionId);

                    return new ClientTrainingShortResonse
                    {
                        ID = t.Id,
                        Title = t.Title,
                        DurationInMinutes = t.DurationInMinutes,
                        TrenerFullName = trener
                            .Let(tr => tr.Surname + " " + tr.Name + (tr.Patronymic is null ? "" : " " + tr.Patronymic)),
                        StartDate = t.StartDate,
                        SectionName = section.Name,
                        GroupName = group.Name
                    };
                }
                )));
        }
        
        /// <summary>
        /// Возвращает подробную информацию о тренировке.
        /// </summary>
        /// <param name="trainingID"></param>
        /// <returns></returns>
        [HttpGet("training-details/{trainingID}")]
        public async Task<IActionResult> GetClientTrainingDetails(int trainingID)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);
            var client = await _clientsRepo.GetClientByID(id);

            if (client is null) return Unauthorized();

            var training = await _trainingsRepository.GetTraining(trainingID);
            if (training is null) return BadRequest("Не удалось найти тренировку.");

            var group = await _groupsRepository.GetGroup(training.GroupId);
            var trener = await _trenersRepository.GetTrener(group.TrenerId);
            var section = await _sectionsRepository.GetSection(group.SectionId);

            return Ok(new ClientTrainingDetailResponse
            {
                ID = training.Id,
                Title = training.Title,
                Room = training.Room,
                Description = training.Description,
                SectionName = section.Name,
                StartDate = training.StartDate,
                DurationInMinutes = training.DurationInMinutes,
                TrenerFullName = trener
                    .Let(tr => tr.Surname + " " + tr.Name + (tr.Patronymic is null ? "" : " " + tr.Patronymic)),
            });
        }

        /// <summary>
        /// Возвращает подробную информацию о секции.
        /// </summary>
        /// <param name="sectionID"></param>
        /// <returns></returns>
        [HttpGet("section-details")]
        public async Task<IActionResult> GetSectionDetailsForClient(int sectionID)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);
            var client = await _clientsRepo.GetClientByID(id);

            if (client is null) return Unauthorized();

            var section = await _sectionsRepository.GetSection(sectionID);

            if (section is null) return Ok();

            return Ok(new ClientSectionDetailResponse
            {
                ID = section.Id,
                SectionName = section.Name,
                Description = section.Description,
                Price = section.Price,
                IsSubscribed = client.Subscriptions?.Any(s => s.SectionId == sectionID) ?? false
            });
        }

        /// <summary>
        /// Возвращает список абонементов клинета.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("subscriptions")]
        public async Task<IActionResult> GetClientSubscriptions()
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}

