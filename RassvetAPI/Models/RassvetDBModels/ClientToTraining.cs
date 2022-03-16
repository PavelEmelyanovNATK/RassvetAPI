using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Models.RassvetDBModels
{
    public partial class ClientToTraining
    {
        public int ClientId { get; set; }
        public int TrainingId { get; set; }

        public virtual ClientInfo Client { get; set; }
        public virtual Training Training { get; set; }
    }
}
