using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Models.RassvetDBModels
{
    public class Training
    {
        public Training()
        {
            ClientToTrainings = new HashSet<ClientToTraining>();
            TrenerToTrainings = new HashSet<TrenerToTraining>();
        }

        public int Id { get; set; }
        public int SectionId { get; set; }
        public string Title { get; set; }
        public string Room { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public int DurationInMinutes { get; set; }

        public virtual Section Section { get; set; }
        public virtual ICollection<ClientToTraining> ClientToTrainings { get; set; }
        public virtual ICollection<TrenerToTraining> TrenerToTrainings { get; set; }
    }
}
