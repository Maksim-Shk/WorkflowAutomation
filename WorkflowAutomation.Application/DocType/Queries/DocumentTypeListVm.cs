using System.Collections.Generic;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.DocType.Queries.GetDocumentTypeListQuery
{
    public class DocumentTypeListVm
    {
        public IList<DocumentTypeListLookupDto> DocumentTypes { get; set; }
    }
}
