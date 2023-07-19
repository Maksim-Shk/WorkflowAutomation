using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowAutomation.Application.Documents.Queries.GetDocumentsStatistics
{
    public class StatisticSets
    {
        public string GroupName { get; set; }

        public List<DocumentStatisticsDto> StatisticSet { get; set; }
    }
}
