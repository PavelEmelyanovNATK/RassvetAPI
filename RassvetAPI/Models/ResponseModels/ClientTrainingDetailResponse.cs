using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Models.ResponseModels
{
    public class ClientTrainingDetailResponse
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Room { get; set; }

        public string Description { get; set; }

        public string SectionName { get; set; }

        public DateTime StartDate { get; set; }

        public int DurationInMinutes { get; set; }

        public string TrenerFullName { get; set; }
    }
}
