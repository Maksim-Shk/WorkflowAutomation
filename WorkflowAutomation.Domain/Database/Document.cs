namespace WorkflowAutomation.Domain;

public partial class Document
{
    public Document()
    {
        DocumentContents = new HashSet<DocumentContent>();
        DocumentStatuses = new HashSet<DocumentStatus>();
    }

    public int IdDocument { get; set; }
    public int IdDocumentType { get; set; }
    public string? Title { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? RemoveDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string IdSender { get; set; } = null!;
    public string IdReceiver { get; set; } = null!;

    public virtual DocumentType IdDocumentTypeNavigation { get; set; } = null!;
    public virtual AppUser IdReceiverNavigation { get; set; } = null!;
    public virtual AppUser IdSenderNavigation { get; set; } = null!;
    public virtual ICollection<DocumentContent> DocumentContents { get; set; }
    public virtual ICollection<DocumentStatus> DocumentStatuses { get; set; }
}
