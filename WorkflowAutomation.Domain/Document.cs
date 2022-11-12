using System;
using System.Collections.Generic;

namespace WorkflowAutomation.Domain
{
    public partial class Document
    {
        public Document()
        {
            DocumentContents = new HashSet<DocumentContent>();
            DocumentUsers = new HashSet<DocumentUser>();
        }

        public int IdDocument { get; set; }
        public int IdDocumentType { get; set; }
        public int IdStatus { get; set; }
        public int IdRoute { get; set; }
        public string? Title { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? RemoveDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual DocumentType IdDocumentTypeNavigation { get; set; } = null!;
        public virtual Route IdRouteNavigation { get; set; } = null!;
        public virtual Status IdStatusNavigation { get; set; } = null!;
        public virtual ICollection<DocumentContent> DocumentContents { get; set; }
        public virtual ICollection<DocumentUser> DocumentUsers { get; set; }
    }
}
