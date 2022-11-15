using System;
using System.Collections.Generic;

namespace WorkflowAutomation.Domain;

public partial class User
{
    public Guid IdUser { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public DateTime? LastOnline { get; set; }

    public DateTime RegisterDate { get; set; }

    public DateTime? RemovalDate { get; set; }

    public virtual ICollection<Document> DocumentIdReceiverNavigations { get; } = new List<Document>();

    public virtual ICollection<Document> DocumentIdSenderNavigations { get; } = new List<Document>();

    public virtual ICollection<DocumentStatus> DocumentStatuses { get; } = new List<DocumentStatus>();

    public virtual ICollection<UserPosition> UserPositions { get; } = new List<UserPosition>();

    public virtual ICollection<UserSubdivision> UserSubdivisions { get; } = new List<UserSubdivision>();
}
