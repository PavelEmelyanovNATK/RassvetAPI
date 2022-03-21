using System;
using System.Collections.Generic;

#nullable disable

namespace RassvetAPI.Models.RassvetDBModels
{
    public partial class ClientToGroup
    {
        public int ClientId { get; set; }
        public int GroupId { get; set; }

        public virtual ClientInfo Client { get; set; }
        public virtual SectionGroup Group { get; set; }
    }
}
