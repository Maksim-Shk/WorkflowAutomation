using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowAutomation.Application.Documents.Queries.GetRecentActivityDocuments
{
    public class RecentActivityDocumentLookupDto
    {
        public int? Id { get; set; }
        public string? Description { get; set; }
        public string? Content { set; get; }
        public DateTime? Date { get;set; }
    }
}
