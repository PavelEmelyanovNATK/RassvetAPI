using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RassvetAPI.Models.ResponseModels;
using RassvetAPI.Services.OrderHandler;
using RassvetAPI.Services.TrainingsRepository;
using RassvetAPI.Services.UsersRepository;
using RassvetAPI.Util.UsefulExtensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Controllers
{
    [Authorize(Roles = "Manager, Admin")]
    [ApiController]
    [Route("managment")]
    public class ManagmentController : ApiControllerBase
    {
        private readonly IOrderHandler _orderHandler;
        private readonly IUsersRepository _usersRepository;
        private readonly ITrainingsRepository _trainingsRepository;

        public ManagmentController(
            IOrderHandler orderHandler,
            IUsersRepository usersRepository,
            ITrainingsRepository trainingsRepository)
        {
            _orderHandler = orderHandler;
            _usersRepository = usersRepository;
            _trainingsRepository = trainingsRepository;
        }

        [HttpGet("process-order/{orderId}")]
        public async Task<IActionResult> ProcessOrder(int orderId)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);
            var user = await _usersRepository.GetUserByIDAsync(id);

            if (user is null) return Unauthorized();

            try
            {
                await _orderHandler.ProcessOrderAsync(orderId, user.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.WrapArray());
            }

            return Ok();
        }

        [HttpGet("group-trainings/{groupId}")]
        public async Task<IActionResult> GetSectionsTrainings(int groupId)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);
            var user = await _usersRepository.GetUserByIDAsync(id);

            if (user is null) return Unauthorized();

            var trainings = await _trainingsRepository.GetGroupTrainingsAsync(groupId);
            return Ok(trainings
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
                }));
        }

        [HttpGet("client-trainings/{clientId}")]
        public async Task<IActionResult> GetCientTrainings(int clientId)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);
            var user = await _usersRepository.GetUserByIDAsync(id);

            if (user is null) return Unauthorized();

            var trainings = await _trainingsRepository.GetClientTrainingsAsync(clientId);

            return Ok(trainings
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
                }));
        }
    }
}
