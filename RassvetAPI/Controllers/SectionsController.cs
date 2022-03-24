using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RassvetAPI.Models.ResponseModels;
using RassvetAPI.Services.SectionsRepository;
using RassvetAPI.Util.UsefulExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Controllers
{
    [ApiController]
    public class SectionsController : ControllerBase
    {
        private readonly ISectionsRepository _sectionsRepository;

        public SectionsController(ISectionsRepository sectionsRepository)
        {
            this._sectionsRepository = sectionsRepository;
        }

        /// <summary>
        /// Возвращает список секций, доступных в системе.
        /// </summary>
        /// <returns>Список с короткой информацией о каждой секции.</returns>
        [HttpGet("sections")]
        public async Task<IActionResult> GetSections()
        {
            var sections = await _sectionsRepository.GetAllSectionsAsync();

            return Ok(sections?.Select(s =>
                new SectionShortResponse
                {
                    ID = s.Id,
                    SectionName = s.Name
                }
            ));
        }
        
        /// <summary>
        /// Возвращает полную информацию о секции.
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        [HttpGet("sectionDetails")]
        public async Task<IActionResult> GetSectionDetails(int sectionId)
        {
            var section = await _sectionsRepository.GetSectionAsync(sectionId);

            return Ok(section?
                .Let(s => new SectionDetailResponse
                {
                    ID = s.Id,
                    SectionName = s.Name,
                    Description = s.Description,
                    Price = s.Price
                }));
        }
    }
}
