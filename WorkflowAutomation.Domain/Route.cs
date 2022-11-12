using System;
using System.Collections.Generic;

namespace WorkflowAutomation.Domain
{
    public partial class Route
    {
        public Route()
        {
            Documents = new HashSet<Document>();
        }

        public int IdRoute { get; set; }
        public string? Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? CompleteDate { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}
