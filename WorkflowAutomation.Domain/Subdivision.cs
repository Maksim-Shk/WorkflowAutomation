using System;
using System.Collections.Generic;

namespace WorkflowAutomation.Domain;

public partial class Subdivision
{
    public int IdSubdivision { get; set; }

    public string Name { get; set; } = null!;

    public int? IdSubordination { get; set; }

    public virtual Subdivision? IdSubordinationNavigation { get; set; }

    public virtual ICollection<Subdivision> InverseIdSubordinationNavigation { get; } = new List<Subdivision>();

    public virtual ICollection<UserSubdivision> UserSubdivisions { get; } = new List<UserSubdivision>();
}
