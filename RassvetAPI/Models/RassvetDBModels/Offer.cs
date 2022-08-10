using System;
using System.Collections.Generic;

#nullable disable

namespace RassvetAPI.Models.RassvetDBModels
{
    public partial class Offer
    {
        public Offer()
        {
            SubscriptionOrders = new HashSet<SubscriptionOrder>();
        }

        public int Id { get; set; }
        public int SectionId { get; set; }
        public int OfferTypeId { get; set; }
        public double Price { get; set; }

        public virtual OfferType OfferType { get; set; }
        public virtual Section Section { get; set; }
        public virtual ICollection<SubscriptionOrder> SubscriptionOrders { get; set; }
    }
}
