using System;
using System.Collections.Generic;

namespace WorkflowAutomation.Domain;

public partial class Document
{
    public int IdDocument { get; set; }

    public int IdDocumentType { get; set; }

    public string? Title { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? RemoveDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public Guid IdSender { get; set; }

    public Guid IdReceiver { get; set; }

    public virtual ICollection<DocumentContent> DocumentContents { get; } = new List<DocumentContent>();

    public virtual ICollection<DocumentStatus> DocumentStatuses { get; } = new List<DocumentStatus>();

    public virtual DocumentType IdDocumentTypeNavigation { get; set; } = null!;

    public virtual User IdReceiverNavigation { get; set; } = null!;

    public virtual User IdSenderNavigation { get; set; } = null!;
}
