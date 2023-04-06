using System;
using System.Collections.Generic;

namespace WorkflowAutomation.Domain
{
    public partial class Position
    {
        public Position()
        {
            InverseIdSubordinationNavigation = new HashSet<Position>();
            UserPositions = new HashSet<UserPosition>();
        }

        public int IdPosition { get; set; }
        public string Name { get; set; } = null!;
        public int? IdSubordination { get; set; }
        public string? ShortName { get; set; }

        public virtual Position? IdSubordinationNavigation { get; set; }
        public virtual ICollection<Position> InverseIdSubordinationNavigation { get; set; }
        public virtual ICollection<UserPosition> UserPositions { get; set; }
    }
}
