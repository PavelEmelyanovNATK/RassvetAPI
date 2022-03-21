using System;
using System.Collections.Generic;

#nullable disable

namespace RassvetAPI.Models.RassvetDBModels
{
    public partial class Bill
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public double Cost { get; set; }
        public int ManagerId { get; set; }

        public virtual ManagerInfo Manager { get; set; }
        public virtual SubscriptionOrder Order { get; set; }
    }
}
