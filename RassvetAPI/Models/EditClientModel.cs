using RassvetAPI.Models.RassvetDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RassvetAPI.Models
{
    public class EditClientModel
    {
        private EditClientModel() { }

        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }

        public static EditClientModel FromClientInfo(ClientInfo client)
        {
            return new EditClientModel
            {
                Surname = client.Surname
                ,Name = client.Name
                ,Patronymic = client.Patronymic
                ,BirthDate = client.BirthDate
            };
        }
    }
}
