namespace WorkflowAutomation.Domain;

public partial class DocumentContent
{
    public int IdDocumentContent { get; set; }
    public int IdDocument { get; set; }
    public string Path { get; set; } = null!;
    public string Name { get; set; } = null!;

    public virtual Document IdDocumentNavigation { get; set; } = null!;
}
