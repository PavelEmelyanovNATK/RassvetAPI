using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Models.ResponseModels
{
    public class ClientTrainingShortResonse
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public int SectionId { get; set; }

        public string GroupName { get; set; }

        public DateTime StartDate { get; set; }

        public int DurationInMinutes { get; set; }

        public string TrainerFullName { get; set; }
    }
}
