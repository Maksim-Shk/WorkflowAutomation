namespace WorkflowAutomation.Domain;

public partial class Subdivision
{
    public Subdivision()
    {
        InverseIdSubordinationNavigation = new HashSet<Subdivision>();
        UserSubdivisions = new HashSet<UserSubdivision>();
    }

    public int IdSubdivision { get; set; }
    public string Name { get; set; } = null!;
    public int? IdSubordination { get; set; }
    public string? ShortName { get; set; }
    public DateTime? CreationDate { get; set; }

    public virtual Subdivision? IdSubordinationNavigation { get; set; }
    public virtual ICollection<Subdivision> InverseIdSubordinationNavigation { get; set; }
    public virtual ICollection<UserSubdivision> UserSubdivisions { get; set; }
}
