using System;
using System.Collections.Generic;

#nullable disable

namespace RassvetAPI.Models.RassvetDBModels
{
    public partial class ManagerInfo
    {
        public ManagerInfo()
        {
            Bills = new HashSet<Bill>();
        }

        public int UserId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
    }
}
