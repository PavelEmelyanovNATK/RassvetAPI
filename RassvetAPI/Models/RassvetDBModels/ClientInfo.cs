using System;
using System.Collections.Generic;

#nullable disable

namespace RassvetAPI.Models.RassvetDBModels
{
    public partial class ClientInfo
    {
        public ClientInfo()
        {
            ClientToSections = new HashSet<ClientToSection>();
            ClientToTrainings = new HashSet<ClientToTraining>();
        }

        public int UserId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? RegistrationDate { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<ClientToSection> ClientToSections { get; set; }
        public virtual ICollection<ClientToTraining> ClientToTrainings { get; set; }
    }
}
