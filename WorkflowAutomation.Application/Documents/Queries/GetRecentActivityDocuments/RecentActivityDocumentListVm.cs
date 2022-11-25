using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowAutomation.Application.Documents.Queries.GetRecentActivityDocuments
{
    public class RecentActivityDocumentListVm
    {
        public IList<RecentActivityDocumentLookupDto> RecentDocuments { get; set; }
    }
}
