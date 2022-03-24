using System;
using System.Collections.Generic;

#nullable disable

namespace RassvetAPI.Models.RassvetDBModels
{
    public partial class SectionGroup
    {
        public SectionGroup()
        {
            ClientToGroups = new HashSet<ClientToGroup>();
            Trainings = new HashSet<Training>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int SectionId { get; set; }
        public int TrenerId { get; set; }

        public virtual Section Section { get; set; }
        public virtual TrenerInfo Trener { get; set; }
        public virtual ICollection<ClientToGroup> ClientToGroups { get; set; }
        public virtual ICollection<Training> Trainings { get; set; }
    }
}
