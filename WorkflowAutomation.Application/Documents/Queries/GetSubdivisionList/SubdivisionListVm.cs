using System.Collections.Generic;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Documents.Queries.GetSubdivisionList
{
    public class SubdivisionListVm
    {
        public IList<SubdivisionListLookupDto> Subdivisions { get; set; }
    }
}
