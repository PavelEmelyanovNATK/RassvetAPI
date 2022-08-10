using System;
using System.Collections.Generic;

#nullable disable

namespace RassvetAPI.Models.RassvetDBModels
{
    public partial class Section
    {
        public Section()
        {
            Offers = new HashSet<Offer>();
            SectionGroups = new HashSet<SectionGroup>();
            Subscriptions = new HashSet<Subscription>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }
        public virtual ICollection<SectionGroup> SectionGroups { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
