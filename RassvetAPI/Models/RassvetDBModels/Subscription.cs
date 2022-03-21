using System;
using System.Collections.Generic;

#nullable disable

namespace RassvetAPI.Models.RassvetDBModels
{
    public partial class Subscription
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public int ClientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public virtual ClientInfo Client { get; set; }
        public virtual Section Section { get; set; }
    }
}
