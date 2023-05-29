using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowAutomation.Application.Documents.Queries.GetDocumentStatisticBySubdivision
{
    public class StatisticSubdivisionSet
    {
        public string GroupName { get; set; }

        public List<DocumentBySubdivisionDto> StatisticSet { get; set; }
    }
}
