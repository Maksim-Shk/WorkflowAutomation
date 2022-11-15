using System;
using System.Collections.Generic;

namespace WorkflowAutomation.Domain;

public partial class DocumentType
{
    public int IdDocumentType { get; set; }

    public string Name { get; set; } = null!;

    public int? IdSubordination { get; set; }

    public virtual ICollection<Document> Documents { get; } = new List<Document>();

    public virtual DocumentType? IdSubordinationNavigation { get; set; }

    public virtual ICollection<DocumentType> InverseIdSubordinationNavigation { get; } = new List<DocumentType>();
}
