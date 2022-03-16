using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RassvetAPI.Models;
using RassvetAPI.Models.RassvetDBModels;
using RassvetAPI.Models.ResponseModels;
using RassvetAPI.Services.ClientsRepository;
using RassvetAPI.Services.RassvetDBRepository;
using RassvetAPI.Services.SectionsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Controllers
{
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientsRepository _clientsRepo;
        private readonly ISectionsRepository _sectionsRepository;

        public ClientController(IClientsRepository clientsRepo, ISectionsRepository sectionsRepository)
        {
            _clientsRepo = clientsRepo;
            _sectionsRepository = sectionsRepository;
        }


        [Authorize(Roles = "Client")]
        [HttpGet("clientInfo")]
        public async Task<IActionResult> GetClientInfo()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);
            var client = await _clientsRepo.GetByID(id);

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

        [Authorize(Roles = "Client")]
        [HttpGet("clientSections")]
        public async Task<IActionResult> GetClientSections()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);
            var client = await _clientsRepo.GetByID(id);

            if (client is null) return Unauthorized();

            if (client.ClientToSections is null) return Ok();

            var sections = client.ClientToSections.Select(it => new SectionShortResponse
            {
                ID = it.Section.Id,
                SectionName = it.Section.Name
            });

            return Ok(sections);
        }

        [Authorize(Roles = "Client")]
        [HttpGet("clientActiveTrainings")]
        public async Task<IActionResult> GetClientActiveTrainings()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);
            var client = await _clientsRepo.GetByID(id);

            if (client is null) return Unauthorized();

            if (client.ClientToTrainings is null) return Ok();

            var trainings = new List<ClientTrainingShortResonse>();

            TrenerInfo trener;

            foreach (var item in client.ClientToTrainings)
            {
                if (item.Training.StartDate.AddMinutes(item.Training.DurationInMinutes) >= DateTime.Now)
                    continue;

                trener = item.Training?.TrenerToTrainings?.FirstOrDefault()?.Trener;

                var trenerFullName = "";
                if (trener != null)
                {
                    trenerFullName = trener.Surname + " " + trener.Name;
                }

                trainings.Add(new ClientTrainingShortResonse
                {
                    ID = item.Training.Id,
                    Title = item.Training.Title,
                    DurationInMinutes = item.Training.DurationInMinutes,
                    TrenerFullName = trenerFullName,
                    StartDate = item.Training.StartDate,
                    SectionName = item.Training.Section.Name
                });
            }

            return Ok(trainings);
        }

        [Authorize(Roles = "Client")]
        [HttpGet("clientPastTrainings")]
        public async Task<IActionResult> GetClientPastTrainings()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);
            var client = await _clientsRepo.GetByID(id);

            if (client is null) return Unauthorized();

            if (client.ClientToTrainings is null) return Ok();

            var trainings = new List<ClientTrainingShortResonse>();

            TrenerInfo trener;

            foreach (var item in client.ClientToTrainings)
            {
                if (item.Training.StartDate.AddMinutes(item.Training.DurationInMinutes) < DateTime.Now)
                    continue;

                trener = item.Training?.TrenerToTrainings?.FirstOrDefault()?.Trener;

                var trenerFullName = "";
                if (trener != null)
                {
                    trenerFullName = trener.Surname + " " + trener.Name;
                }

                trainings.Add(new ClientTrainingShortResonse
                {
                    ID = item.Training.Id,
                    Title = item.Training.Title,
                    DurationInMinutes = item.Training.DurationInMinutes,
                    TrenerFullName = trenerFullName,
                    StartDate = item.Training.StartDate,
                    SectionName = item.Training.Section.Name
                });
            }

            return Ok(trainings);
        }

        [Authorize(Roles = "Client")]
        [HttpGet("clientTrainingDetails")]
        public async Task<IActionResult> GetClientTrainingDetails(int trainingID)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);
            var client = await _clientsRepo.GetByID(id);

            if (client is null) return Unauthorized();

            if (client.ClientToTrainings is null) return Ok();

            var item = client.ClientToTrainings.FirstOrDefault(ct => ct.TrainingId == trainingID);

            if (item is null) return Ok();

            var trener = item.Training?.TrenerToTrainings?.FirstOrDefault()?.Trener;

            var trenerFullName = "";
            if (trener != null)
            {
                trenerFullName = trener.Surname + " " + trener.Name;
            }

            return Ok(new ClientTrainingDetailResponse {
                ID = item.Training.Id,
                Title = item.Training.Title,
                Room = item.Training.Room,
                DurationInMinutes = item.Training.DurationInMinutes,
                Description = item.Training.Description,
                TrenerFullName = trenerFullName,
                StartDate = item.Training.StartDate,
                SectionName = item.Training.Section.Name
            });
        }

        [Authorize(Roles = "Client")]
        [HttpGet("sectionDetailsForClient")]
        public async Task<IActionResult> GetSectionDetailsForClient(int sectionID)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("ID").Value);
            var client = await _clientsRepo.GetByID(id);

            if (client is null) return Unauthorized();

            var section = await _sectionsRepository.GetSection(sectionID);

            if (section is null) return Ok();

            return Ok(new ClientSectionDetailResponse
            {
                ID = section.Id,
                SectionName = section.Name,
                Description = section.Description,
                Price = section.Price,
                IsSubscribed = client?.ClientToSections?.Any(s => s.SectionId == section.Id) ?? false
            });
        }
    }
}

