using System;
using System.Collections.Generic;

namespace WorkflowAutomation.Domain.Database
{
    public partial class AllowedSubdivision
    {
        public int IdSubdivision { get; set; }
        public string Name { get; set; } = null!;
        public int? IdSubordination { get; set; }
    }
}
