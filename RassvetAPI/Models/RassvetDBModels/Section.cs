using System;
using System.Collections.Generic;

#nullable disable

namespace RassvetAPI.Models.RassvetDBModels
{
    public partial class Section
    {
        public Section()
        {
            SectionGroups = new HashSet<SectionGroup>();
            SubscriptionOrders = new HashSet<SubscriptionOrder>();
            Subscriptions = new HashSet<Subscription>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public virtual ICollection<SectionGroup> SectionGroups { get; set; }
        public virtual ICollection<SubscriptionOrder> SubscriptionOrders { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
