using System;
using System.Collections.Generic;

namespace WorkflowAutomation.Domain
{
    public partial class UserPosition
    {
        public int IdUserPosition { get; set; }
        public int IdPosition { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime? RemovalDate { get; set; }
        public string IdUser { get; set; } = null!;

        public virtual Position IdPositionNavigation { get; set; } = null!;
        public virtual AppUser IdUserNavigation { get; set; } = null!;
    }
}
