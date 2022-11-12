using System;
using System.Collections.Generic;

namespace WorkflowAutomation.Domain
{
    public partial class DocumentType
    {
        public DocumentType()
        {
            Documents = new HashSet<Document>();
        }

        public int IdDocumentType { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Document> Documents { get; set; }
    }
}
