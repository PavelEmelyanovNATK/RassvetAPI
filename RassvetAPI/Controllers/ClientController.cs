using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RassvetAPI.Models.RequestModels;
using RassvetAPI.Models.ResponseModels;
using RassvetAPI.Services.ClientsRepository;
using RassvetAPI.Services.OrderHandler;
using RassvetAPI.Services.SectionsRepository;
using RassvetAPI.Services.TrainingsRepository;
using RassvetAPI.Util;
using RassvetAPI.Util.UsefulExtensions;
using System;
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
        private readonly IClientsRepository _clientsRepository;
        private readonly ISectionsRepository _sectionsRepository;
        private readonly ITrainingsRepository _trainingsRepository;
        private readonly IOrderHandler _orderHandler;

        public ClientController(
            IClientsRepository clientsRepo,
            ISectionsRepository sectionsRepository,
            ITrainingsRepository trainingsRepository,
            IOrderHandler orderHandler
            )
        {
            _clientsRepository = clientsRepo;
            _sectionsRepository = sectionsRepository;
            _trainingsRepository = trainingsRepository;
            _orderHandler = orderHandler;
        }

        /// <summary>
        /// Возвращает всю информацию о клинете.
        /// </summary>
        /// <returns></returns>
        [HttpGet("info")]
        public async Task<IActionResult> GetClientInfoAsync()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);
            var client = await _clientsRepository.GetClientByIDAsync(id);

            if (client is null) return Unauthorized();

            var clientInfo = new ClientInfoResponse
            {
                ID = client.UserId,
                Surname = client.Surname,
                Name = client.Name,
                Patronymic = client.Patronymic,
                BirthDate = client.BirthDate,
                RegistrationDate = client.RegistrationDate
            };

            return Ok(ResponseBuilder.Create(code: 200, data: clientInfo));
        }

        /*Пересмотреть решение о необходимости этого метода*/
        /// <summary>
        /// Возвращает список секций, в которых состоит клиент.
        /// </summary>
        /// <returns></returns>
        [HttpGet("sections")]
        public async Task<IActionResult> GetClientSectionsAsync()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);
            var client = await _clientsRepository.GetClientByIDAsync(id);

            if (client is null) return Unauthorized();

            var sections = (await _sectionsRepository.GetClientSectionsAsync(id))
                ?.Select(s =>
                new SectionShortResponse
                {
                    ID = s.Id,
                    SectionName = s.Name
                });

            return Ok(ResponseBuilder.Create(code: 200, data: sections));
        }

        /// <summary>
        /// Возвращает список предстоящих тренрировок клинета.
        /// </summary>
        /// <returns>Список элементов с короткой информацией о тренировке.</returns>
        [HttpGet("active-trainings")]
        public async Task<IActionResult> GetClientActiveTrainingsAsync()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);
            var client = await _clientsRepository.GetClientByIDAsync(id);

            if (client is null) return Unauthorized();

            var trainings = (await _trainingsRepository.GetClientActiveTrainingsAsync(id))
                ?.Select(t =>
                new ClientTrainingShortResonse
                {
                    ID = t.Id,
                    Title = t.Title,
                    DurationInMinutes = t.DurationInMinutes,
                    TrainerFullName = t.Group.Trener
                            .Let(tr => tr.Surname + " " + tr.Name + (tr.Patronymic is null ? "" : " " + tr.Patronymic)),
                    StartDate = t.StartDate,
                    SectionId = t.Group.SectionId,
                    GroupName = t.Group.Name
                });

            return Ok(ResponseBuilder.Create(code: 200, data: trainings));
        }

        [HttpGet("active-trainings/{sectionId}")]
        public async Task<IActionResult> GetClientActiveTrainingsBySectionAsync(int sectionId)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);
            var client = await _clientsRepository.GetClientByIDAsync(id);

            if (client is null) return Unauthorized();

            var trainings = (await _trainingsRepository.GetClientActiveTrainingsBySectionAsync(id, sectionId))
                ?.Select(t =>
                new ClientTrainingShortResonse
                {
                    ID = t.Id,
                    Title = t.Title,
                    DurationInMinutes = t.DurationInMinutes,
                    TrainerFullName = t.Group.Trener
                            .Let(tr => tr.Surname + " " + tr.Name + (tr.Patronymic is null ? "" : " " + tr.Patronymic)),
                    StartDate = t.StartDate,
                    SectionId = t.Group.SectionId,
                    GroupName = t.Group.Name
                });

            return Ok(ResponseBuilder.Create(code: 200, data: trainings));
        }

        /// <summary>
        /// Возвращает список прошедших тренировок клинета.
        /// </summary>
        /// <returns>Список элементов с короткой информацией о тренировке.</returns>
        [HttpGet("past-trainings/{pagesCount}")]
        public async Task<IActionResult> GetClientPastTrainingsAsync(int pagesCount = 1)
        {
            if (pagesCount < 1) pagesCount = 1;
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);
            var client = await _clientsRepository.GetClientByIDAsync(id);

            if (client is null) return Unauthorized();

            var trainings = (await _trainingsRepository.GetClientPastTrainingsAsync(id, pagesCount))
                ?.Select(t =>
                new ClientTrainingShortResonse
                {
                    ID = t.Id,
                    Title = t.Title,
                    DurationInMinutes = t.DurationInMinutes,
                    TrainerFullName = t.Group.Trener
                            .Let(tr => tr.Surname + " " + tr.Name + (tr.Patronymic is null ? "" : " " + tr.Patronymic)),
                    StartDate = t.StartDate,
                    SectionId = t.Group.SectionId,
                    GroupName = t.Group.Name
                });

            return Ok(ResponseBuilder.Create(code: 200, data: trainings));
        }

        [HttpGet("past-training/{sectionId}/{pagesCount}")]
        public async Task<IActionResult> GetClientPastTrainingsAsync(int sectionId, int pagesCount = 1)
        {
            if (pagesCount < 1) pagesCount = 1;
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);
            var client = await _clientsRepository.GetClientByIDAsync(id);

            if (client is null) return Unauthorized();

            var trainings = (await _trainingsRepository.GetClientPastTrainingsBySectionAsync(id, sectionId, pagesCount))
                ?.Select(t =>
                new ClientTrainingShortResonse
                {
                    ID = t.Id,
                    Title = t.Title,
                    DurationInMinutes = t.DurationInMinutes,
                    TrainerFullName = t.Group.Trener
                            .Let(tr => tr.Surname + " " + tr.Name + (tr.Patronymic is null ? "" : " " + tr.Patronymic)),
                    StartDate = t.StartDate,
                    SectionId = t.Group.SectionId,
                    GroupName = t.Group.Name
                });

            return Ok(ResponseBuilder.Create(code: 200, data: trainings));
        }

        /// <summary>
        /// Возвращает подробную информацию о тренировке.
        /// </summary>
        /// <param name="trainingID"></param>
        /// <returns></returns>
        [HttpGet("training-details/{trainingID}")]
        public async Task<IActionResult> GetClientTrainingDetailsAsync(int trainingID)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);
            var client = await _clientsRepository.GetClientByIDAsync(id);

            if (client is null) return Unauthorized();

            var training = (await _trainingsRepository.GetTrainingAsync(trainingID)) 
                ?.Let( t => new ClientTrainingDetailResponse
                {
                    ID = t.Id,
                    Title = t.Title,
                    Room = t.Room,
                    Description = t.Description,
                    SectionName = t.Group.Section.Name,
                    StartDate = t.StartDate,
                    DurationInMinutes = t.DurationInMinutes,
                    TrainerFullName = t.Group.Trener
                        .Let(tr => tr.Surname + " " + tr.Name + (tr.Patronymic is null ? "" : " " + tr.Patronymic)),
                });
            if (training is null) 
                return Ok(ResponseBuilder.Create(code: 404, errors: "Не удалось найти тренировку."));

            return Ok(ResponseBuilder.Create(code: 200, data: training));
        }

        /// <summary>
        /// Возвращает подробную информацию о секции.
        /// </summary>
        /// <param name="sectionID"></param>
        /// <returns></returns>
        [HttpGet("section-details/{sectionID}")]
        public async Task<IActionResult> GetSectionDetailsForClientAsync(int sectionID)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);
            var client = await _clientsRepository.GetClientByIDAsync(id);

            if (client is null) return Unauthorized();

            var section = (await _sectionsRepository.GetSectionAsync(sectionID))
                ?.Let(s => new ClientSectionDetailResponse
                {
                    ID = s.Id,
                    SectionName = s.Name,
                    Description = s.Description,
                    Price = s.Price,
                    IsSubscribed = client.Subscriptions?.Any(s => s.SectionId == sectionID) ?? false
                });

            return Ok(ResponseBuilder.Create(code: 200, data: section));
        }

        /// <summary>
        /// Возвращает список абонементов клинета.
        /// </summary>
        /// <returns></returns>
        [HttpGet("subscriptions")]
        public async Task<IActionResult> GetClientSubscriptionsAsync()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);
            var client = await _clientsRepository.GetClientByIDAsync(id);

            if (client is null) return Unauthorized();

            var subscriptions = client.Subscriptions
                .Select(s =>
                new SubscriptionResponse
                {
                    ID = s.Id,
                    BarcodeString = $"4399{s.Id:00000000}",
                    SectionName = s.Section.Name,
                    StartDate = s.StartDate,
                    ExpirationDate = s.ExpirationDate,
                });

            return Ok(ResponseBuilder.Create(code: 200, data: subscriptions));
        }

        [HttpPost("make-order")]
        public async Task<IActionResult> MakeSubscriptionOrderAsync(SubscriptionOrderRequest subscriptionOrder)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);
            var client = await _clientsRepository.GetClientByIDAsync(id);

            if (client is null) return Unauthorized();

            try
            {
                await _orderHandler.MakeOrderAsync(subscriptionOrder.OfferID, client.UserId);
            }
            catch (Exception ex)
            {
                return Ok(ResponseBuilder.Create(code: 400, data: ex.Message));
            }

            return Ok(ResponseBuilder.Create(code:200));
        }
    }
}

