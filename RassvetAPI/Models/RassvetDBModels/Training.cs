using System;
using System.Collections.Generic;

#nullable disable

namespace RassvetAPI.Models.RassvetDBModels
{
    public partial class Training
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Title { get; set; }
        public string Room { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public int DurationInMinutes { get; set; }

        public virtual SectionGroup Group { get; set; }
    }
}
