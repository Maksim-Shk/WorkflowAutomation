using System;
using System.Collections.Generic;

namespace WorkflowAutomation.Domain
{
    public partial class DocumentUser
    {
        public DocumentUser()
        {
            InversePreviousDocumentUserNavigation = new HashSet<DocumentUser>();
        }

        public int IdDocumentUser { get; set; }
        public int IdDocument { get; set; }
        public int? PreviousDocumentUser { get; set; }
        public Guid IdSender { get; set; }
        public Guid IdReceiver { get; set; }

        public virtual Document IdDocumentNavigation { get; set; } = null!;
        public virtual User IdReceiverNavigation { get; set; } = null!;
        public virtual User IdSenderNavigation { get; set; } = null!;
        public virtual DocumentUser? PreviousDocumentUserNavigation { get; set; }
        public virtual ICollection<DocumentUser> InversePreviousDocumentUserNavigation { get; set; }
    }
}
