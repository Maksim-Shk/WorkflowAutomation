using System;
using System.Collections.Generic;

namespace WorkflowAutomation.Domain;

public partial class Position
{
    public int IdPosition { get; set; }

    public string Name { get; set; } = null!;

    public int? IdSubordination { get; set; }

    public virtual Position? IdSubordinationNavigation { get; set; }

    public virtual ICollection<Position> InverseIdSubordinationNavigation { get; } = new List<Position>();

    public virtual ICollection<UserPosition> UserPositions { get; } = new List<UserPosition>();
}
