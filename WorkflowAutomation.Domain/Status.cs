using System;
using System.Collections.Generic;

namespace WorkflowAutomation.Domain;

public partial class Status
{
    public int IdStatus { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<DocumentStatus> DocumentStatuses { get; } = new List<DocumentStatus>();
}
