using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Models.RassvetDBModels
{
    public partial class TrenerToTraining
    {
        public int TrenerId { get; set; }
        public int TrainingId { get; set; }

        public virtual Training Training { get; set; }
        public virtual TrenerInfo Trener { get; set; }
    }
}
