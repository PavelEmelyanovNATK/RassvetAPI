using System;
using System.Collections.Generic;

#nullable disable

namespace RassvetAPI.Models.RassvetDBModels
{
    public partial class TrenerInfo
    {
        public TrenerInfo()
        {
            SectionGroups = new HashSet<SectionGroup>();
        }

        public int UserId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<SectionGroup> SectionGroups { get; set; }
    }
}
