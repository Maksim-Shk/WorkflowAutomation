using System;
using System.Collections.Generic;

namespace WorkflowAutomation.Domain
{
    public partial class UserSubdivision
    {
        public int IdUserSubdivision { get; set; }
        public int IdSubdivision { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime? RemovalDate { get; set; }
        public Guid IdUser { get; set; }

        public virtual Subdivision IdSubdivisionNavigation { get; set; } = null!;
        public virtual User IdUserNavigation { get; set; } = null!;
    }
}
