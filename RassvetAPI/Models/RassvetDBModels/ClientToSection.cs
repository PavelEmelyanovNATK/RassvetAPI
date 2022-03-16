using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Models.RassvetDBModels
{
    public class ClientToSection
    {
        public int ClientId { get; set; }
        public int SectionId { get; set; }

        public virtual ClientInfo Client { get; set; }
        public virtual Section Section { get; set; }
    }
}
