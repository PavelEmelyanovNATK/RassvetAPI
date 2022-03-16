using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Models.ResponseModels
{
    public class SectionDetailResponse
    {
        public int ID { get; set; }
        public string SectionName { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
