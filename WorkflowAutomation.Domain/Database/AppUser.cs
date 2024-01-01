namespace WorkflowAutomation.Domain;

public partial class AppUser
{
    public AppUser()
    {
        DocumentIdReceiverNavigations = new HashSet<Document>();
        DocumentIdSenderNavigations = new HashSet<Document>();
        DocumentStatuses = new HashSet<DocumentStatus>();
        UserPositions = new HashSet<UserPosition>();
        UserSubdivisions = new HashSet<UserSubdivision>();
    }

    public string IdUser { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string? Patronymic { get; set; }
    public DateTime? LastOnline { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime? RemovalDate { get; set; }

    public virtual AspNetUser IdUserNavigation { get; set; } = null!;
    public virtual ICollection<Document> DocumentIdReceiverNavigations { get; set; }
    public virtual ICollection<Document> DocumentIdSenderNavigations { get; set; }
    public virtual ICollection<DocumentStatus> DocumentStatuses { get; set; }
    public virtual ICollection<UserPosition> UserPositions { get; set; }
    public virtual ICollection<UserSubdivision> UserSubdivisions { get; set; }
}
