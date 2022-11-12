using System;
using System.Collections.Generic;

namespace WorkflowAutomation.Domain
{
    public partial class User
    {
        public User()
        {
            DocumentUserIdReceiverNavigations = new HashSet<DocumentUser>();
            DocumentUserIdSenderNavigations = new HashSet<DocumentUser>();
            UserPositions = new HashSet<UserPosition>();
            UserSubdivisions = new HashSet<UserSubdivision>();
        }

        public Guid IdUser { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Patronymic { get; set; }
        public DateTime? LastOnline { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? RemovalDate { get; set; }

        public virtual ICollection<DocumentUser> DocumentUserIdReceiverNavigations { get; set; }
        public virtual ICollection<DocumentUser> DocumentUserIdSenderNavigations { get; set; }
        public virtual ICollection<UserPosition> UserPositions { get; set; }
        public virtual ICollection<UserSubdivision> UserSubdivisions { get; set; }
    }
}
