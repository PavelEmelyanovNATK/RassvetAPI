using System;
using System.Collections.Generic;

#nullable disable

namespace RassvetAPI.Models.RassvetDBModels
{
    public partial class ClientInfo
    {
        public ClientInfo()
        {
            ClientToGroups = new HashSet<ClientToGroup>();
            SubscriptionOrders = new HashSet<SubscriptionOrder>();
            Subscriptions = new HashSet<Subscription>();
        }

        public int UserId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? RegistrationDate { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<ClientToGroup> ClientToGroups { get; set; }
        public virtual ICollection<SubscriptionOrder> SubscriptionOrders { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
