using System;
using System.Collections.Generic;

#nullable disable

namespace RassvetAPI.Models.RassvetDBModels
{
    public partial class OfferType
    {
        public OfferType()
        {
            Offers = new HashSet<Offer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }
    }
}
