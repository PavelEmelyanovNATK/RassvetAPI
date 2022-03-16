using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RassvetAPI.Models.ResponseModels;
using RassvetAPI.Services.SectionsRepository;
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

        [HttpGet("sections")]
        public async Task<IActionResult> GetSections()
        {
            var sections = await _sectionsRepository.GetAllSections();

            if (sections is null) return Ok();

            var sectionsShortInfo = new List<SectionShortResponse>();

            foreach(var section in sections)
            {
                sectionsShortInfo.Add(new SectionShortResponse
                {
                    ID = section.Id,
                    SectionName = section.Name
                });
            }

            return Ok(DateTime.Now);
        }

        [HttpGet("sectionDetails")]
        public async Task<IActionResult> GetSectionDetails(int sectionId)
        {
            var section = await _sectionsRepository.GetSection(sectionId);

            if (section is null) return Ok();

            return Ok(new SectionDetailResponse
            {
                ID = section.Id,
                SectionName = section.Name,
                Description = section.Description,
                Price = section.Price
            });
        }
    }
}
