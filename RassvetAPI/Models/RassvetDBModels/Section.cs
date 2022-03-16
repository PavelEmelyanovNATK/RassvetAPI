using System;
using System.Collections.Generic;

#nullable disable

namespace RassvetAPI.Models.RassvetDBModels
{
    public partial class Section
    {
        public Section()
        {
            ClientToSections = new HashSet<ClientToSection>();
            Trainings = new HashSet<Training>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public virtual ICollection<ClientToSection> ClientToSections { get; set; }
        public virtual ICollection<Training> Trainings { get; set; }
    }
}
