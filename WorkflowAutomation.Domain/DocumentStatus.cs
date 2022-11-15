using System;
using System.Collections.Generic;

namespace WorkflowAutomation.Domain;

public partial class DocumentStatus
{
    public int IdDocumentStatus { get; set; }

    public int IdDocument { get; set; }

    public int IdStatus { get; set; }

    public DateTime AppropriationDate { get; set; }

    public Guid IdUser { get; set; }

    public virtual Document IdDocumentNavigation { get; set; } = null!;

    public virtual Status IdStatusNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
