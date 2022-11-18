using System;
using System.Collections.Generic;

namespace WorkflowAutomation.Domain
{
    public partial class Status
    {
        public Status()
        {
            DocumentStatuses = new HashSet<DocumentStatus>();
        }

        public int IdStatus { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<DocumentStatus> DocumentStatuses { get; set; }
    }
}
